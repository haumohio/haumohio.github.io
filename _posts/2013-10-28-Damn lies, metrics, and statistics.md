---
title: Damn lies, metrics, and statistics
description: You can't fix it, if you don't measure it
layout: post
tags: [agile, metrics, quality]
image: measurement.jpg
---

_Today's thought was inspired by a tweet by [@jeremywaite] via [@csterwa] reminding me of a Dilbert cartoon on the use of metrics by 'management'._

[![CENTERED](/assets/post_images/lies_dil1.png)][dil1]

One constant in my career has been the need to measure. While working in a sawmilling technology research team at Scion in the late 90s
 my catch-cry to a very traditional industry became "You can't fix it, if you don't measure it". The sentiment is the same in our 
software industry too. If a change is made to software without the means to measure the effect then all that can be done is to hope 
that everything is as intended.

If you're living in the land of the hopeful and don't know where to start then I suggest looking at something simple, direct, 
and most importantly easy to automate. There's no point in starting out on this endeavour by creating something that is 
onerous to perform and maintain, as it just won't last.

For instance, code coverage of unit tests is built-in to many systems. Set up a continuous integration system to build the code, 
run the unit tests on every commit, and publish the coverage somewhere visible. Even better publish the trend over many commits. 
With this simple metric you now know which pieces of your code need the most tender loving care.

While we are on the topic of unit tests, a unit test itself is a kind of measurement. For instance, when a developer is fixing a bug, 
how do you measure when the bug is fixed? The simple answer is with a unit test or two. And if (when?) the test is performed in a 
continuous integration build the bug's eradication is "measured" every single time. The same can be said for performance and security tests. 
Do you run these tests often enough on ALL of the code-base so that you can see a trend and be alerted to reductions?

Now, the flip side to adding measurements and metrics is "Why are you measuring that?" Metrics, like the code coverage trend, 
exist for a reason: to invoke change. If you have a couple of hundred metrics calculated every build that are all displayed 
on a dozen wall-screens and generating multiple emails a day to every developer, then the team will get overloaded and will 
simply shut their eyes and ears to the constant noise. There may need to be some focus on the metrics that drive improvement 
on the current hot topics.

In our example of code coverage, it is usually used most effectively as an alert. Low coverage for an application or module 
alerts the team that there isn't much proof of the logic being correct, whereas high coverage doesn't really prove anything 
as other quality questions such as dynamic interactions become more interesting.

My final comment is that metrics and trends exist to remind us what we should and shouldn't be doing, but if they become a 
target then the metric becomes an end rather than a means to improving quality.

[![CENTERED](/assets/post_images/lies_dil2.png)][dil2]

#### Don't let the metrics beat you!

_Originally posted on [Assurity's website]_

[dil1]:http://dilbert.com/strips/comic/2007-05-16/
[dil2]:http://dilbert.com/strips/comic/1994-11-21/
[@jeremywaite]:https://twitter.com/jeremywaite
[@csterwa]:https://twitter.com/csterwa
[Assurity's website]:http://assurity.co.nz
