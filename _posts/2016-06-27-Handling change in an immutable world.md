---
title: ""
description: Handling change in an immutable world
layout: post
tags: [F#, coding, immutability]
image: Constant change.png
excerpt_separator: <!--more-->
---

> My name is Pete, and I’m addicted to F#.

{:class="excerpt"}
I admit it — I love using functional languages and, given my .Net back-ground, particularly F#. The simplicity and above all readability puts it at the top of the heap. Most of what I’ll be talking about generally applies to most functional languages. I’ll use F# in my examples, but anyone familiar with the ML family should be able to follow along without a problem.
<!--more-->

One of the founding concepts of the language and on the ML family of functional programming languages (e.g. Haskel, OCaml,…) is the immutability of variables (the name of which is now pretty ironic as they don’t vary anymore). Immutability makes a lot of things easier for me as a programmer — no pass-by-ref accidental value changes, automatic thread safety, one name = one value within scope.

This is all very beautiful and easy in the examples where we grab some data from a cool data source like the Star Wars social network, play with it a bit, and display it. However, things are pretty boring without any interaction, and interaction usually means changing (or at least saving) some data.


> But, how do I interact with the program to change immutable data?

This is a question that I struggled with when coming to grips with this way of programming so I am __documenting my discoveries__ for my own benefit, and sharing it for the benefit of those on a similar journey.

The standard answer to the above question is __you don’t__. Instead you make a new object that is like the old object but different.

    person {                 olderPerson {                 
        name = “Fred”    -->      name = "Fred"
        age = 42                  age = 43
    }                        }

This is where F# can help us with the first technique for handling change…

### Record constructors — Just like that one but…

F# has a concept of a record, which is similar to a struct in C# in that:

- It’s immutable (funny that)
- It is a value type (not passed by reference)
- It can be compared to other records of the same type for equality by comparing its members’ values (i.e. compare the name and the age values)
- Records have a simple definition…

    type Person = { name: string; age: int; rating: double }

… and a simple instantiation.

    let p1 = { name = "Pete"; age = 42; rating = 0.0 }

This is all good, but what about handling change? The record also has an __alternate constructor__ that goes like this:

    let p2 = { p1 with age = 43; rating = p1.rating * 1.2 }

This quickly lets you create a complex object that is a copy of the original with a few tweaks. You can even reference the original object within the constructor, as I did with the _rating value_. This is particularly useful for appending to contained lists.

Of course, now you have two values; the original one and the “changed” one. You can just ignore the original and let it eventually fall out of scope. Although, sometimes it is useful to have a breadcrumb trail of changes for logging / auditing / debugging purposes.

### What’s next

In the next little while I will move on to a few other discoveries and up the complexity a bit.