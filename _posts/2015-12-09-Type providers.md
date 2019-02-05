---
title: Creating a Generative Type Provider in F#
description: The trips and traps
layout: post
tags: [Neo4j, coding, F#, "Type Providers"]
image: DuckTyping.png
excerpt_separator: <!--more-->
---

This is the story of how I came to write a generative type provider for Neo4J, and the trips and traps I came across on the way.
 <!--more-->

This isn't the story I was originally intending to write for 2015’s F# advent calendar. I was originally going to talk about my currently ongoing adventures with F# vs. Linux vs. Azure, but it doesn't yet have a satisfactory ending — so stay tuned!


### The Mission
To be able to write queries for Neo4J that can be compile-time checked

### TL; DR
For those who want the quick version:

- The Provided Types API is OO and mutable-focused
- Nearly there is still valuable
- Erased types look like their base types from the outside
- Provided types can’t be Records
- Erased types simply aren't visible to generic serializers via reflection
- Only add the root type(s) to a generated assembly, and only after all child types have been added to it
- To store instance values — add a private field to each instance and access it via a central register of field definitions

### The Problem
Neo4J has a fantastic query language called Cypher, with an accompanying library for .NET. However, in order to use cypher a programmer needs to write a lot of the query as a string.

    db.Cypher.Match("p:Person").Where("p.born=1973").Return("p")

As you can see the return type (_Person_) and the query filter are unchecked strings even on the simplest of queries, and the return type is dynamic which can be troublesome in F#.

Wouldn't it be great if we could have something that give us node and relationship names and types, similar to the way that the SQL type provider gives us tables and columns.

### The Vision
When the Neo4J client tried to populate the property using reflection it just sees the base type without any generated propertiesSo, let’s follow the pattern that the SQL type provider give us for instantiation.

    type schema = Haumohio.Neo4j.Schema<"http://localhost:7474/db">
    let db = Neo4jClient.GraphClient(connectionFromConfig)
    db.Connect()

We create a schema at compile-time against an active Neo4J database, and then connect to one with a “matching” schema at run-time.

### Getting Started

If you've never delved into the realm of Type Providers, get the “Provided Types” helpers from somewhere like this and read Mavvn’s blog and this blog on functional flow. This provides the framework for building your own provider.

### Why Generative?

So, I started off building a simple Erased type provider. But it only got me as far as expressing node and relationship names. This was great - I got data. But I really wanted to get return types with named properties as well, so I could use it as a useful object store.

The difference between this and a Generative one is that the erased one is compiled into the user’s assembly when used and is somewhat ephemeral. A generative provider’s type has it’s own assembly, and therefore the provided types can be referenced by C#, and (importantly in my case) serialized to from JSON/XML.

### From the Top

The core of the top level of the code is to compose the node labels, relationship names, node property names, and node proxy types as members of the schema type that we’re trying to provide.

    let cx = connectToNeo4j()
    makeType asm "Schema"
        |> addMember (makeInclType "Labels" |> addMembers ( cx.labelList |> toStaticProps ))
        |> addMember (makeInclType "Rels" |> addMembers ( cx.relList |> toStaticProps ))
        |> addMember (makeInclType "Props" |> addMembers (graphTypes cx))
        |> addMember (makeInclType "Proxies" |> addMembers (graphProxies cx))
        |> addIncludedType

> Lesson 1: The Provided Types API is OO and mutable-focused

I created a few simple “helpers” to mutate and pass on the type-structure (basically an apply) like addMember(s) and addIncludedType to help with the functional style.

### The simple stuff : Names of things

This is the node, property, and relationship names. It’s simple because they are all implemented as a static properties that return their own name as the value, just like you find in the Getting Started tutorials.

    let createStaticProp name =
        ProvidedProperty(name, typeof<string>,
                            IsStatic = true,
                            GetterCode = (fun args -> <@@ name @@>))

The properties are only slightly more complex, as they are a collection of static properties named after the node that the properties are members.

    let makePropContainer name =
    makeIncludedType name
        |> addMembers (connection.propList name |> toStaticProps)

> Lesson 2: Nearly there is still valuable

At this point I was still having fun. It all compiled and worked as an erased type provider without spending a great deal of time on it — about two sessions.

I could now write a query like below:

    type myPersonType = { name:string; born:int; }
    db.Cypher
        .Match("p", schema.Labels.Person)
        .Where( "p." + schema.Props.Person.born + "=1973" )
        .Return<myPersonType>("p")
        .Results

… but I had to define by own type (with the property names spelled exactly right) to serialize into , or use dynamic typing.

### Filling types of thin air
So, I jumped into creating erased proxy types that could be used in the Return command’s type parameter.

Of course, the properties couldn't be static as the values needed to be different for each instance. I tried a number of increasingly nasty techniques to store values such as a hidden static dictionary with a combined key of object ID and property name, with mixed results.

Finally I cobbled something together for a single named property and got a sort-of unexpected result. The results returned the correct number of obj objects with no values! This is because the erased types are actually just the concrete type that the provided type is based upon, with a bit of decoration.

> Lesson 3: Erased types look like their base types from the outside

My next gambit was to base the proxies on a type that has more stuff to hang values off. I created a record type with a name, id, a property hashmap, and getters/setters and based the proxy types on this.

> Lesson 4: Provided types can’t be Records

At least that was true at the time — there has been discussions and this may or may not have been changed now. In any case, I changed it into a class and tried again. This time I got back the correct number of my special type, but with no values set, and came to the conclusion that the generated properties weren't being activated by the Neo4J client’s deserialization routines.

> Lesson 5: Erased types simply aren’t visible to generic serializers via reflection

The big switch
As I said before, the biggest difference with a generative type provider is that it lives in it’s own assembly. Generally, it can be a temporary file to be compiled in.

    let provAsm = 
        Path.GetTempFileName()
        |> Path.ChangeExtension(, ".dll")
        |> ProvidedAssembly

After a lot of reading (and re-reading) through Dave Fancher’s post I got something going. The trick was to add the created types to the created assembly once, and only after everything had been finished.

    let internal addIncludedType (ty:ProvidedTypeDefinition) =
        provAsm.AddTypes([ty]) 
        ty
    ...
    do
        this.AddNamespace(ns, [addIncludedType schema])

> Lesson 6: Only add the root type(s) to a generated assembly, and only after all child types have been added to it

### The Instance Variables
This was the bit I got stuck on earlier, and still caused me some headaches, but Dave Fancher came to my rescue here too with private fields.

> Lesson 7: To store instance values — add a private field to each instance and access it via a central register of field definitions

I was on the right track by storing “stuff” in a mutable property list by type. But I also needed a private field attached to each instance. Then I could create a non-static property with a getter and a setter that alters/accesses this field via the list of field definitions.

    let mutable private propsByType : (string * ProvidedField) list = []
    let inline makeProvidedPrivateReadonlyField<'T> fieldName =
        let field = ProvidedField(fieldName, typeof<'T>)
        field.SetFieldAttributes(FieldAttributes.Private )
        field
    let initField nameOfEnclosingType  =
        let newdic = (ProvidedField("_myProps", typeof<Dictionary<string,string>>))
        let added = propsByType @ [(nameOfEnclosingType, newdic)]
        propsByType <- added

I could then add initField to the constructor invoke code, and we’re away!

### The Generated Proxies
Once I had a system for generating non-static types I could finally provide the types to deserialize the data into.

let graphProxies neo4j =
    (graph.findNodes neo4j 
    |> List.map 
        (fun name -> 
            makeIncludedType name
            |> addMember (myPropsField name)
            |> addMembers (graph.propNames name neo4j |> List.map ( fun nm -> createProp nm name ))
            |> addMember (createCtor name)
        )
    )

### Using the provider
The goal of the provider was to take some of the “magic strings” out of using Neo4J in F#, and to provide the types that match the data.

    let data = 
        db.Cypher
            .Match("p", schema.Labels.Person)
            .Where( fun p -> p.born=1973 )
            .Return<schema.Proxies.Person>("p")
            .Limit(Nullable<int>(10))
            .Results
    printfn "%A" data.[0].name

You can see in the example above that the name of the type “Person”, as well as the data-shaped type schema.Proxies.Person (and it’s properties — e.g. name & born) has been provided to give us some compile-time confidence that the code matches the data. As an added bonus we can use a F# filter function in the Where clause, too.

### Moving on
This type provider is open-source for your perusal at https://github.com/claruspeter/neo4jtypeprovider. Please feel free to fork and use as you like.
