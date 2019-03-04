---
title: Haumohio Test
description: Software development and consulting 
layout: sectioned
class: christchurch
layoutclass: half-n-half sectioned
---

{% for sect in site.contents %}

<section class="bg-orange" style='overflow:auto;'>
 {{sect.content}}
</section>

{% endfor %} 

