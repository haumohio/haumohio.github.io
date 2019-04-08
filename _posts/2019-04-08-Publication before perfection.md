---
title: Publication before Perfection
description: Building the right thing before building it all
layout: post
tags: [Continuous Delivery, Feedback]
navbar-class: inverse
image: wireframe.png
excerpt_separator: <!--more-->
---

Every project should ideally start off with considering how to show something to the stakeholders in the first day, and providing a mechanism to allow those stakeholders to provide feedback as simply as possible.

{:class="shadowed"}
![feedback loop - build, measure, learn](/assets/post_images/feedback_loop.png)

<!--more-->

## Background

By way of a bit of background this post was inspired by a tweet by Jason Gorman:

[![What are the 'basics' of software development (after programming?) What should we expect every developer be able to do?](/assets/post_images/basicsoftware.png)](https://twitter.com/jasongorman/status/1114781031574405120){:target="_blank"}{:class="shadowed"}

A lot of people said "version control" and "people skills", which are really good answers, but only a couple even mentioned anything to do with delivery of the software to the customer.

{:class="image quarter shadowed"}
![cd](/assets/images/Agile Principles-Deliver.png)


One of the four modern agile principles is to _Deliver Value Continuously_.  The ultimate conclusion would therefore be to deliver value on the first day   
... to the customer   
... in a form that can be easily consumed  
... and provide feedback on.

{:class="image right"}
[![incremental car](https://blog.crisp.se/wp-content/uploads/2016/01/mvp.png)](https://blog.crisp.se/2016/01/25/henrikkniberg/making-sense-of-mvp){:target="_blank"}

I'm reminded of the classic iterative development graphic by Henrik Kniberg in his blog post on minimum viable products.  The progress pictured can't  sanely be achieved without a regular conversations with the people who are involved in using the product.


## So what?

One of the first things to consider when starting a software project is deciding on what the delivery pipeline to the customer is, what feedback channels can be provided to the customer, and how can we implement that today.  

Coming back to Jason's tweet, that means that developers would benefit from developing and exercising the skills required to quickly set up a delivery pipeline. We could probably even have a set of scripts or templates that can create the "normal" pipeline for the particular business, so that when a new product/project comes up then it's a no-brainer to quickly run up a new product environment that works just like all the previous projects.

![Old fashioned Jenkins Pipeline](/assets/post_images/jenkins.png)
Good old Jenkins pipeline showing each commit that may fail test stages on the way to production
{:class="caption"}

{:title="In my humble opinion..."}
## IMHO 

Personally, the most powerfully engaged projects have been when I (as part of a dev team) have setup a project that pushes an empty templated solution to some kind of Demo environment on the same day of the project kick-off meeting, and then told the stakeholders that they can see the progress _here_, and what they can expect to see tomorrow.  

Straight away, people can see something happening while the project is still top-of-mind, and can quite often spark conversations about the value the project can offer the business.