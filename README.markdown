# HtmlShouldExtensions

Extension methods, in the style of [Should](https://github.com/erichexter/Should/), that allow developers
to make assertions about HTML fragments

## A couple of simple examples

``` C#
var fragment = "<p class=\"para\">lorem ipsum</p>";
fragment.ShouldMatchSelector("p.para").WithInnerHtml("lorem ipsum");
```

or

``` C#
var fragment = "<p class=\"para\">lorem ipsum</p>";
fragment.ShouldMatchSelector("p").WithClass("para");
```

See [the wiki](https://github.com/fourthhospitality/HtmlShouldExtensions/wiki) for further documentation.

## LICENSE

HtmlShouldExtensions is copyright Fourth Hospitality 2012 and is released under the MIT license:

* http://www.opensource.org/licenses/MIT
