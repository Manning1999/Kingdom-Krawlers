*************TwoDimensionalPlayerMovement********************
It was originally only intended for platformers and four directional movement that would have the player look in the direction they were moving so it's mouse based look system (looking where the mouse is regardless of which direction the character is moving) is still a bit buggy but I will fix it up when I get the chance.

The idea for this script was to make a single script that could be used for multiple types of 2D games, from side scrolling platformers like mario, to top down games like the original Zelda games that had four directions to move in.

The Scene has a number of example characters that showcase the different options for the TwoDimensionalPlayerMovement script

Plz forgive the poor animations, they were something I put together just tonight for example purposes. Ideally I would have done the full 8 direction animations but I only had time to do four directions



***************Camera**************
The camera will follow whatever gameobject is set to be the target. 
The camera has the option to follow the target exactly, or to lag behind just a little bit to give a greater sense of movement (how much is lags behind can be altered by changing the "speed" variable. Higher speed = less lag, lower speed = more lag)




****************Interactions***************
There is also an interaction system. In order to make something interactable, all that needs to be done is attach the "StandardInteractable" script to it or a script that inherits from "StandardInteractable" and then the "Interactor" script that is attached to the interactor gameobject on the player will sort out the rest. The interactor uses a 2D trigger collider which pivots so that it is always facing the direction of the mouse cursor.



****************IHurtable*******************
This is an interface that makes it possible to cause damage to anything that implements the IHurtable interface. E.g. if a sword collides with anything, it will check for this interface and then if what it collides with has this interface, it will call the TakeDamage() method on what it has collided with