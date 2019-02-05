---
title: Haumohio
description: Software development and consulting 
layout: default
class: christchurch
layoutclass: half-n-half
---

---

{:class="image right"}
![Programming](./assets/images/dev.png)  

something about development

---

{:class="image"}
![Double0](./assets/images/double0.png)  

Technical consulting and leadership in association with [Double-O Consulting](http://doubleo.nz){:target="_blank"}.  

Specialising in emergent architecture and technical practices such as test driven development.
Particular focus on the Callaghan [Build For Speed](https://www.callaghaninnovation.govt.nz/innovation-skills/build-speed){:target="_blank"} improvement programme for startups.

---

{:class="image right"}
![Code Club](./assets/images/codeclub.png)  

something about code club

---

{:class="image"}
![Pete the Programmer](./assets/images/windmill2.png)  

something about pete the programmer


---

{:class="image right"}
![Blogging](./assets/images/blog.jpg)
{% assign titles = site.posts.first.title | split: '|'%}
{% assign titles = titles | push: site.posts.first.description %}
### [{{ titles  | array_to_sentence_string: "-" }}]({{site.posts.first.url}})

{:class="excerpt"}
({{site.posts.first.date | date_to_string}} - {{site.posts.first.tags | array_to_sentence_string}})

{:class="excerpt"}
{{ site.posts.first.excerpt }}

{:class="excerpt"}
[read more >]({{site.posts.first.url}})

To read {{ site.posts.size }} more posts [Click here >](/blog)
