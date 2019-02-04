---
title: Haumohio Blog
description: a word or two 
layout: default
class: christchurch
---

{% for post in site.posts %}

### [{{ post.title }}]({{post.url}})

{:class="byline"}
({{post.date | date_to_string}} - {{post.tags | array_to_sentence_string}})

{{post.excerpt}}

[read more >]({{post.url}})

{% endfor %}
