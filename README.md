Basic Entitas Monogame Example
==============================

This is a very basic example of how to use
[Entitas](https://github.com/sschmid/Entitas-CSharp) with
[Monogame 3.5](http://www.monogame.net/).

I wanted to try out some different C# Entity-Component-Systems for a
2d game I've been developing, and Entitas looked super exciting, but a
lot of the examples are very Unity-focused.

I put this together in a bit of time mostly following along Entitas'
[wiki](https://github.com/sschmid/Entitas-CSharp/wiki) as well as the
examples in the
[Readme code](https://github.com/sschmid/Entitas-CSharp/tree/master/Readme).

Code Generation
---------------

One of the cooler things about Entitas is its code-generation to give
you an awesome api for adding/removing components, as well as some
other things. I am still figuring out how all of it works, but if you
run this project and supply a ```generate``` parameter, it will
generate all of the component extensions for you. You then have to add
them from the ```Generated/``` directory in the project folder. Once
you've done this you should be able to build and run the game.

Playing
-------

Use the arrow keys to move around. Exciting, huh?
