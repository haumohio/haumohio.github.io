---
title: Blogging
background: url('/assets/images/blog2.png')
---

# A recent blog...

{% assign titles = site.posts.first.title | split: '|'%}
{% assign titles = titles | push: site.posts.first.description %}
### [{{ titles  | array_to_sentence_string: "-" }}]({{site.posts.first.url}})

{:class="excerpt"}
({{site.posts.first.date | date_to_string}} - {{site.posts.first.tags | array_to_sentence_string}})

{:class="excerpt"}
{{ site.posts.first.excerpt }}

{:class="excerpt"}
[Keep reading...]({{site.posts.first.url}})

[Read {{ site.posts.size }} more posts](/blog)