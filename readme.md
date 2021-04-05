# UdonSharp Scripts for use in VRC SDK3 Unity map making
To use these you'll require Udonsharp which can be found [here](https://github.com/MerlinVR/UdonSharp)

## Script usage:
### SkyboxRotate
Attach it to an empty gameobject, and it will slowly rotate the skybox for all players.

### GlobalToggleArray
Will toggle the active state of an array of bojects for all players, and keep it in sync for new players when they join.

### MasterOnlyInteract
Will check if the user interacting with an object is the master, and if they are will call the configuerd event.

### Reset
A script that will move all children objects of the "Parent Object" to the position of the "Reset Location" on an Interact event.

![setup example](https://i.imgur.com/91Bmms0.png "Example setup, to respawn a ball at a reset point")

### EmptyAreaEvent
Set the Trigger to the trigger gameobject you want to check the inside of for players.
Set the Recheck interval to how often you want the script to check for users within the region.
Set the event target and event name, this will be called when no users are found inside the region.
The event is global by defailt, if you want it to be local you can disabled EventIsGlobal

### TimedStartAndEnd
Call StartEvent to start the timer, it will also fire a startevent if provided one.
After timerLength seconds, the end event will fire and the timer will stop.