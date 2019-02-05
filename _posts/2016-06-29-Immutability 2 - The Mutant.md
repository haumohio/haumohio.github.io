---
title: Immutable Change II
description: The Mutant
layout: post
tags: [F#, coding, immutability]
image: mutable.png
excerpt_separator: <!--more-->
---

This is part 2 of a series of handling change within immutability. Today we are going to “cheat” by going against all of our best functional language intentions and actually create a mutable object.

>Why would you intentionally create a mutable object in a language founded on mutability?

Well, let me tell you a story… I was working with a team that used the .Net stack, and C# by default, with a fairly complex inter-related class structure. When .Net 3.5 came around some of us were excited by the lambda functions and linq sub-language, and without knowing it the architecture started looking pretty functional. Once we recognised this, it was an obvious next step to look at F# for some of the logic work. So we ended up with a hybrid application that was __partially OO and partially functional__, which actually worked pretty well for us.

However, when it came to users maintaining the data structures the infrastructure was designed to work with __mutable, persisted classes__. Inevitably, the F# data structures needed to follow suit.

Thankfully, as F# is still part of the .Net family, it can be made to play by these rules too. The language supports __properties with setters__ as well as getters, and highlights the fact that we are mutating an object with a specific syntax; the “<-” symbol.

    myObject.value <- 42

This symbol is especially reserved for changing the value of an existing value, and the value has to be specifically declared as mutable. To create a simple mutable value, you can just add the mutable keyword in the declaration (along with an initial value).

    let mutable x = 0

### Mutable Classes with Properties
After a bit of research we found the F# for fun and profit page on classes. Now we can create a mutable, complex class with automatic properties, like so:

    type MyClass() =
        member val MyProp = 42 with get,set

Also, in the more complex case where you want to use an explicit mutable backing field…

    type MyComplexClass( int initialValue ) =
        let mutable _myProp = initialValue
        member val MyProp =
            with get() = _myProp
                set( newValue ) = _myProp <- newValue

* Note the use of the <- symbol in the setter.

This allowed us to create a mutable class that could be updated by the existing UI layer and passed to the existing persistence layer for saving to the db.

    type Person() =
        member val Name = "" with get,set
        member val Address = "" with get,set
        member val Email = "" with get,set
        member val Age = 0 with get,set
        member val Rating = 0.0 with get,set
    ...
    let p1 = Person()
    p1.Name <- "Fred"
    p1.Email <- "fred@bloggs.com"
    p1.Age <- 42
    p1.Rating <- 7.3
    ui.Edit(p1)             // Callback to the OO UI layer for editing
    match sanitize(p1) with //check for nasties and proceed accordingly
    | Sanitary -> db.Save(p1)
    | Unsanitary err -> ui.DisplayError(err)

As you can see, the code is starting to look a little un-functional and really more like C# code with an F# syntax. This seemed to be an almost inevitable __by-product of interfacing with an OO language__ so closely, at least along the interfaces. However, It did allow us to move incrementally into a better (not biased at all !) language, and as the code moved away from the interface it became more functional and took better advantage of the language.

Ultimate this is my least favourite way of dealing with change, but it can serve a useful purpose.

### Continuing on

This is article #2 in this series. Next time, we'll be looking at a purely functional solution that allows us to more easily update data within a structure - Lenses.
