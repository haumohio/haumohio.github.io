---
title: Immutable Change III
description: Focusing in on change with Lenses
layout: post
tags: [F#, coding, immutability, lenses]
image: magnify code.png
excerpt_separator: <!--more-->
---


This is part 3 of a series of handling change within immutability. So far we've looked at creating complex values based on other complex values and even playing the OO game of creating changeable objects. This time I'm going to focus in on a purely functional technique for creating a complex object that is only different from its predecessor by a value that is deep down inside the object.

>Changing the thing inside the thing inside the other thing

<!--more-->

### The Problem

Let me tell you a story. I was working on a project that needed to include people with contact details — a fairly common scenario. We separated the concerns and kept contact bundle distinct from the main person type and made an explicit address structure with all of its composite parts. We ended up with something like below:

```fsharp
type Address{
  number: int;
  street: string;
  city: string;
  state: string;
  country: string;
}
type ContactDetails {
  cellNumber: string;
  homeNumber: string;
  workNumber: string;
  home: Address option;
  work: Address;
}
type Person = { 
   name: string; 
   age: int; 
   contact: ContactDetails
}
```

It's not a particularly pretty design, nor is it really awful. We have a couple of __levels of complex values that contains other complex values__. Let's consider an example of this structure with some values that we want to make a small change to.

```fsharp
Person{
 name: "Fred"
 age: 42
 contact: 
  {
    cellNumber: "21-555-1234"
    homeNumber: ""
    workNumber: "555-9876"
    work: 
     {
       number: 57
       street: "Colombo St"
       city: "Christchurch"
       state: "Canterbury"
       country: "New Zealand"
     }
    home: None
   }
}
```

We started off with trying to change the street number of the work address to “55” using nested seeded record constructors as explained in part 1 of this series.

```fsharp
let updatedPerson = 
 { myPerson with 
     contact = 
       { myPerson.contact with 
          work = { myPerson.contact.work with number = 55 }
       }         
 }
```
Personally, my _cognitive load_ was now starting to ramp up trying to visualise the small change within the complex object. Also, the levels of reference to seed the work address was getting a bit ridiculous. As we increase nesting levels, it obviously was only going to get worse.

>Challenge: I would like to have a function that takes an embedded complex element, updates it and returns it back as an update in the wider complex object tree.

### Lenses to the rescue
We found a [good starter on lenses](https://bugsquash.blogspot.com/2011/11/lenses-in-f.html){:target="_blank"} by Mauricio Scheffer. A lens is an operational type that has a getter and a setter for a thing-within-a-thing. The getter gets the inner value (b) from the containing value (a), and the setter returns a container with a provided updated inner value.

```fsharp
type Lens<'a, 'b> =
    {
        get: 'a -> 'b
        set: 'b -> 'a -> 'a
    }
```

By extension, we can combine these two operations to “update” a container by adding a member function to our Lens type that takes a transform function for updating the inner value.

```fsharp
// ....continue at end of Lens definition 
    with member this.update (transform: 'b -> 'b) (a: 'a) : 'a =
            let currentInnerValue = x.get a
            let newInnerValue = transform currentInnerValue 
            this.set newInnerValue a
```

With this generic type under our belt we can create a specific lens for each updating operation in our project, such as _Lens<ContactDetails, Address>_.

### The Lens solution
For our particular code we need a lens for the work address. I'm going to bundle all of my specific lenses up into a module to keep things tidy.

```fsharp
module MyLenses =
 let WorkAddress: Lens<ContactDetails, Address> =
  {
    get = fun contact -> contact.work;
    set = fun newAddr contact -> { contact with work = newAddr }
  }
```

Well, what did that achieve? It looks like all I did was write an extra 3 or 4 lines of code to access the address field; something I can do with just a dot! Let's look at the code to update the value now.

```fsharp
let updatedContact = 
   myPerson.contact 
   |> MyLenses.WorkAddress.update 
        (fun addr -> {addr with number = 55})
```

OK — it's maybe a bit clearer, but that's only half the job. I still need to update the person too.

>Lenses can be composed into a chain to dig deeply into a nested value

The __composition of lenses__ is done via an inline operator, usually with the standard symbol "<code>>>|</code>"

```fsharp
let inline (>>|) (lens1: Lens<'a,'b>) (lens2: Lens<'b,'c>) =
  {
    get = lens1.get >> lens2.get
    set = lens2.set >> lens1.update
  }
```

So, now if we create a specific lens for each nesting level; the outer level from _person_ to _contact details_, and the innermost level from _address_ to _street number_…

```fsharp
let Contact: Lens<Person, ContactDetails> =
  {
    get = fun person -> person.contact;
    set = fun newContact person -> {person with contact = newContact }
  }
let StreetNumber: Lens<Address, int> =
  {
    get = fun addr -> person.number;
    set = fun newNumber addr -> {addr with number = newNumber }
  }
```

… we can combine them for a much clearer chain of operation through the nested hierarchy. The specific lenses can even be re-used (similar to the types), so for example the _StreetNumber_ lens may be used on any _address_ value.

The __compound lens__ can now take in a person, navigate through the layers to the _street number_ and set it to “55”.

```fsharp
let compoundLens = Contact >>| WorkAddress >>| StreetNumber
let updatedPerson = myPerson |> compoundLens.set 55
```

The end effect is a __very compact and yet readable__ method of getting into a complex value and “changing” one little bit of it, without sacrificing immutability. On the down side, it does require a fair bit of cruft listing out all of the specific lenses for each field that you may wish to “edit”. On balance we found it to be worth it.

In this article, we have gone through how to construct your own lenses. The code is quite small and portable, but if you prefer there are libraries that contain these lens operators, such as [FSharpX](https://github.com/fsprojects/FSharpx.Extras){:target="_blank"}.

### Continuing on
This is article #3 in this series. Next time, we'll be looking at extending the use of lenses for working with contained collections and the possibility of a lookup returning _None_.