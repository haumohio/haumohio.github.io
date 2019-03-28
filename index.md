---
title: Haumohio
description: Software development and consulting 
layout: sectioned
class: christchurch
layoutclass: sectioned
---

{% for sect in site.contents %}

<section class="{{sect.layoutclass}}" style="background-image: {{sect.background}};" aria-label="{{sect.title}}">
 {{sect.content}}
</section>

{% endfor %} 

