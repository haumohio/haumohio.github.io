---
title: Haumohio
description: Software development and consulting 
layout: sectioned
class: christchurch
layoutclass: sectioned
---
{% assign main_sections = site.contents | where: 'dest', 'main' %}
{% for sect in main_sections %}

<section class="{{sect.layoutclass}}" style="background-image: {{sect.background}};" aria-label="{{sect.title}}">
 {{sect.content}}
</section>

{% endfor %} 

