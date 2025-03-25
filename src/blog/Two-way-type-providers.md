---
layout: blog
title: Two-way type providers
---
# Two-way type providers

The magic of compiled data sources that change value when you look at them

![](images/blog/1_pD0pryn_LdQApRZEbALBhQ.png)

In the previous articles I focused on some patterns that can be used in your own code to manage changing data. This time I’m looking at a mechanism that’s actually **built-in to the F# language**. As such, this may or may not apply to other functional languages.

## Type Providers

> A Type Provider is a feature that produces structures & types at **compile-time** for coding auto-complete and live data access at run-time.

Personally, I struggled with the concept when I came across it. The data source (or at least one that is structurally identical) needs to be **accessible at compile-time** for both the developer and any build server you may have. In fact, if you want to use it for auto-completion of code then you will need access at that point in time too.

Note that if the data source is slow, then compiling/auto-completion will be slowed down too. In fact, if the data source is unavailable (e.g. a third party source that has issues) then compilation will fail.

All-in-all it felt a little _dirty_ to me to couple the data artefacts with the source code so closely, but I have made peace with this requirement in the interest of **keeping data and code in sync** in such an explicit manner. The trick is to use a connection string in the config file that points to an empty canonical data source internally for building, and a live data source (with identical structure!) in production.

## Getting data

For this article I’ll reference the [SQLProvider](http://fsprojects.github.io/SQLProvider/) with SQL Server, as it is very similar to linq-to-sql for .NET developers. It has a data context that follows a repository pattern and even includes method like _SubmitChanges()_ to publish the unit of work in a transaction.

To get the data from a database it is a simple as creating a type that is parameterised with the connection string, referencing the current data context and hitting ‘dot’ to get a list of tables and then columns.

```fsharp
#r "FSharp.Data.SQLProvider.dll"
open FSharp.Data.Sql

type myDB = SqlDataProvider<"connStringName">
let customers = myDB.GetDataContext().Customers
```

This is where the magic happens !  Each time this code is called, the program will get the current data from the table. So is the code mutable or immutable?!? 😉

## Changing the external data

The SqlProvider has pretty good docs, and includes simple [CRUD](http://fsprojects.github.io/SQLProvider/core/crud.html) operations.

```fsharp
type myDB = SqlDataProvider<"connStringName">
let cxt = myDB.GetDataContext()
let customers = cxt.Customers

//change the name of the first customer and save
let cust1 = customers.First()  
cust1.FirstName <- "Peter"  
cxt.SubmitChanges()
let cust1Updated = customers.First()
let SUCCESS = ( cust1Updated.FirstName = "Peter" )
```

As you may have noticed the data that comes from the data source is _mutable._ In order to update the database I need to mutate an element in the context and submit the change … ugh! I have yet to find a simple solution for this that I like.

Note that the above code has an **error** that would make `SUCCESS=false`. The _cxt.Customers_ property has a getter (which is just a pretty function) that may return different values each time, and F# copes with that. However, my _customers_ value is a immutable value that once evaluated doesn’t change. In my early days this kind of bug caused me hours of frustration until I realised that is should be a **function**  *customers()* that would be evaluated each time.

```fsharp
let customers() = cxt.Customers      //This is correct now!
```

## TL;DR

So to summarise…

*   You can couple your code to data sources at compile time using a type provider.
*   Changing the data in the (mutable) db often means changing mutable data in the code.
*   Compiling against slow, remote, or inaccessible data sources may cause fairly predictable problems. Keep a local structural copy for compiling against.

## Continuing on

This is article #5 in this series, and is the last one that talks about data access technologies. Next time I will discuss a few algorithms that take mutation for granted in your standard computer science textbooks.

1.  [Records: Just like the that one, only different.](handling-change-in-an-immutable-world)
2.  [The OO way: implementing property setters in F# to “cheat” by creating a C#-style mutable object.](the-mutant)
3.  [Lenses: changing the thing inside the thing inside the other thing.](focusing-in-on-change-with-lenses)
4.  [Lenses and lists: changing the thing amongst the other things in that thing…maybe](lenses-and-lists)
5.  _Two-way Type Providers: reflecting changing external data_
6.  Some common things that usually rely on mutation: like sorting…