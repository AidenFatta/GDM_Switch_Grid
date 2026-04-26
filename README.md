# GDM_Switch_Grid
Repository for the early development of Switch Grid for the final project of C# Programming offered through Rize Education.

Complete game mechanics, including:

Moving
Shooting
Switching Player Characters
Zooming in and out of the camera
Special abilities (for the one included enemy, but is scalable to handle more in the future)
Level Time Limit
Remapping controls
Currently, there is one simple level to demonstrate the base level of mechanics

Multi-player is not configured yet, as development focused on the single-player to make sure it's complete. Multi-player is built off of singleplayer functions thus must be completed first.

Database saving for progress and times is also not included yet.

Project requirements that are included are:

Delegate Events
Singleton Design Pattern
Core gameplay loop
Functional and Alpha build complete UI
Pause menu
Audio and lighting
Current Bugs:

When holding a button and pulling up the pause menu, the button will continuously be held until another input is read when the menu is closed.
Attempting to bind a key to the key that is already in use for that action will cause the text “Key in use” to be displayed on the remap button for that action until the mapping is changed to a different key.
