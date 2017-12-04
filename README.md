# [Button] attribute for Unity inspector

This repository contains a couple of scripts allowing you to easily add a button to your custom Unity scripts, without the need to write your own custom inspectors.

It's based on [a similar script](https://github.com/zaikman/UnityPublic/blob/master/InspectorButton.cs) by [zaikman](https://github.com/zaikman). But during the years of in-house usage it's extended to a degree that I finally decided to rewrite it completely from scratch, with all the required features already implemented in the initial release.

And to publish it on the GitHub :)

Usage:
```
Button("MethodName" [, string label] [, string tooltip] [, float width] [, float minWidth] [, int fontSize])
```

In the simplest case, you just add a `[Button("MyMethodInThisClass")]` attribute in front of some field - and you're ready to go! _(just add an extra bool field and apply this attribute to it)_

The only things you need to worry about are:
* The class you add a button to is a Unity class _(i.e., inherited from `UnityEngine.Object`: `MonoBehaviour`, `ScriptableObject` etc.)_
* It has the method with this name _(either implemented in the class itself or inherited)_
* The method is of void type and doesn't take any arguments.

In other words, nothing tricky!

Additionally, this `[Button]` attribute provides some very handy extra features:
* __Multi-object editing__: The button is available when multiple objects selected. It's colored in orange to indicate that you're going to modify multiple objects at once.
* __Automatic error-detection__: instead of throwing some meaningless errors to the console, the button simply gets disabled when something is wrong, and also tries to describe the problem in the tooltip. Speaking of that...
* __[Optional] Tooltip__. As the attribute argument, instead of separate attribute.
* __[Optional] Button width__. With only two float arguments, you can describe almost any interactive button width you may want.
It may **auto-generate the size** of the button to fit the text. It may be **absolute** _(in pixels)_ or **relative** _(stretching/shrinking depending on the inspector width)_. In short, no more need to assume the font size at the user's machine or to put up with ugly "fast-programmer-style" layout.
* __[Optional] Font size__. You can make some buttons bigger by increasing their font size.
* __Wrapping text__. If text is too long to fit into one line _(because of hard-coded button width or too thin inspector window)_, the button automatically enables text wrapping and strethes vertically to provide enough height for text.

Each of the above features are usually taken as granted, but anyone who tried to create custom inspectors can prove how "fun" it is to implement this stuff.

If you think some important features are still missing, feel free to create a pull request. :)
