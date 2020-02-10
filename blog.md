---
title: Haumohio Blog
description: a word or two 
layout: default
layoutclass: cappedwidth
class: christchurch
---

{% for post in site.posts %}
{% assign titles = post.title | split: '|'%}
{% assign titles = titles | push: post.description %}
### [{{ titles  | array_to_sentence_string: "-" }}]({{post.url}})

{:class="byline"}
({{post.date | date_to_string}} with tags {{post.tags | array_to_sentence_string}})

{:class="published{{post.published}}"}
{{post.excerpt}}

[read more >]({{post.url}})

{% endfor %}
