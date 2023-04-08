# My Old Unity Scripts
## These scripts were created in 2017 when I was just starting to learn Unity.

# BallScript
The BallScript class is a MonoBehaviour that controls a ball object in a 2D game. The class has the following private variables:

ballIsActive: a boolean that tracks whether the ball is currently in play
ballPosition: a Vector3 that stores the current position of the ball
ballInitialForce: a Vector2 that represents the initial force to apply to the ball when it is launched
audioSource: an AudioSource component that plays sound effects when the ball collides with other objects
The class also has the following public and serialized variables:

force_x: a float that determines the horizontal force to apply to the ball
force_y: a float that determines the vertical force to apply to the ball
playerObject: a GameObject that represents the player object that controls the ball
hitSound: an AudioClip that represents the sound effect to play when the ball collides with other objects
The class has the following methods:

Start(): initializes the ball's initial force, position, and audio source component
Update(): handles the ball's movement and behavior during gameplay. If the player hits the spacebar, the ball is launched with the initial force. If the ball is not in play, it follows the player's position until launched. If the ball goes out of bounds, it is reset to the player's position, the player loses a life, and the ball's velocity is reset to zero.
OnCollisionEnter2D(Collision2D collision): plays the hit sound effect when the ball collides with other objects.

# BlockScript
The BlockScript class is a MonoBehaviour that represents a block object in a 2D game. The class has the following private variable:

numberOfHits: an integer that tracks the number of times the block has been hit by a ball.
The class also has the following public and serialized variables:

hitsToKill: an integer that represents the number of hits required to destroy the block.
points: an integer that represents the number of points the player receives when the block is destroyed.
The class has the following method:

OnCollisionEnter2D(Collision2D collision): is called when the block collides with another object in the scene. If the object is a Ball (based on its tag), the numberOfHits variable is incremented. If numberOfHits is equal to hitsToKill, the player object is found (based on its tag), and the AddPoints() method is called to add the points to the player's score. Finally, the block object is destroyed using Destroy(this.gameObject).

# Human_01_Work
This is a script for a character in a game that can find and perform work tasks. Here is a summary of the main components of the script:

Variables:

home: a Vector3 that represents the character's home position
target: a Vector3 that represents the position of the current task the character is assigned to
distance: a float that represents the distance between the character and their current target
_mass: an array of GameObjects that represent the available work tasks
min: a float that holds the current minimum distance to a work task
current: a float that holds the current distance to a work task being checked
numberTarget: an integer that holds the index of the current work task being targeted
findComplite: a boolean that indicates whether the character has found a task yet
WorkYes: a boolean that indicates whether the character has started working on a task
ResFull: a boolean that indicates whether the character has completed a task and is returning home
timer: a float that counts the time since the character has completed a task
agent: a NavMeshAgent component that handles the character's movement
Start() method:

Gets the character's home position and NavMeshAgent component
Update() method:

Checks if the character has not found a task yet and if they are a "CompleteUnit" (presumably a specific type of character)
Calls FindWork() to find a nearby work task
Sets findComplite to true
Checks if the character has found a task and started working on it
Calculates the distance to the current target
If the character has not reached the target and the resource is not full, sets the NavMeshAgent destination and sets the character's animation to "walk_1"
If the character has reached the target and the resource is not full, increments the timer and sets the character's animation to "lum"
If timer is greater than 5 seconds, sets ResFull to true, sets target to the character's home position, and destroys the current work task
If the character has not reached the target and the resource is full, sets the NavMeshAgent destination and sets the character's animation to "walk_2"
If the character has reached the target and the resource is full, sets ResFull to false, calls FindWork() to find a new work task, sets the NavMeshAgent destination, and sets the character's animation to "idle"
FindWork() method:

Finds all GameObjects with the "Tree" tag and stores them in the _mass array
Iterates through the array and finds the closest work task
Sets target to the closest work task position and numberTarget to the index of the closest work task
OnDrawGizmos() method:

Draws lines in the editor to visualize the character's home position and current target position

# HumanController
This is a script for controlling the animations of a humanoid character in a game.

The HumanController class has an enumeration TTT that defines four different animation types: idle, lum, walk_1, and walk_2. The typeAnim variable of type TTT is used to store the current animation state.

The Start method initializes the typeAnim variable to idle, and gets the Animator component attached to the character.

The Update method uses a switch statement to set the appropriate animation parameters in the Animator component based on the typeAnim variable.

Each case sets different boolean parameters on the Animator component to play the corresponding animation. In addition, it sets the visibility of different game objects that represent the character's tools or accessories, such as a bag or a tool, by enabling or disabling them with SetActive.

For example, when typeAnim is walk_1, the character's Walk parameter is set to true, the Idle and Lum parameters are set to false, and the _tood game object is set to be visible while the _hand and _bag objects are set to be hidden.

This script would typically be attached to the same GameObject as the Animator component.

# HumanUnit
This is a script written in C# for a Unity game. Here is an overview of what the script does:

The script defines various materials used for different parts of a character's model.
The script has references to various game objects, including the character model, weapons, beard, hair, and bag.
The script has two BoxColliders, which are used for collision detection.
The script has several tags, including "CreateUnit" and "CompleteUnit".
In the Update() function, the script checks if the game object has the "CreateUnit" tag and whether the global variable "onTriggerUnit" is true or false. If "onTriggerUnit" is false, the game object and its components are given the Ghost material. If "onTriggerUnit" is true, the game object and its components are given the GhostNone material.
The script also checks if the global variable "activeCreateUnit" is false. If so, the game object's tag is changed to "CompleteUnit", and one of the BoxColliders is enabled while the other is disabled. The UnitIDSignaliz() function is also called.
The script also checks if the game object is selected and whether it has the "CompleteUnit" tag. If so, the game object and its components are given the Selected materials. If the game object is not selected, it is given the Normal materials.
The OnTriggerEnter() and OnTriggerExit() functions change the global variable "onTriggerUnit" to true or false, respectively.
The UnitIDSignaliz() function assigns a unique ID to the game object and sets the corresponding element in the global array "UnitID" to the same value.
Overall, the script is responsible for managing the appearance and collision detection of a character in a game, as well as assigning a unique ID to each character.

# MapGen
This is a C# script for generating a 3D mesh in Unity, along with a texture to apply to it. The script defines a class called MapGen, which inherits from the MonoBehaviour class in Unity.

The script has several member variables, including size_x and size_z, which determine the width and height of the mesh, and tileSize, which determines the size of each individual square in the mesh. There is also a public variable called mapSize, which is an instance of the MapGeneration class.

The Start method is called when the script is first loaded. It sets the size_x, size_z, and tileSize variables based on the values of the mapSize object, and then calls the BuildMesh and BuildTexture methods.

The BuildTexture method creates a new Texture2D object with the same width and height as the mesh, and then sets the color of each pixel in the texture to a specific color. Finally, the method sets the texture as the main texture of the MeshRenderer component.

The BuildMesh method generates the vertices, normals, and UV coordinates for the mesh, as well as the indices that define the triangles. It then creates a new Mesh object, sets its properties to the generated data, and assigns it to the MeshFilter and MeshCollider components.

Overall, this script generates a basic mesh with a flat surface and a single texture applied to it. The size and shape of the mesh can be adjusted by changing the values of the size_x, size_z, and tileSize variables.

# MapGeneration
This script is a C# script for generating a random map with walls and smoothing the generated map. The script is attached to a GameObject in Unity, and it uses the MeshGeneration component to create a mesh for the generated map.

The script has several public variables that can be set in the Unity editor. The sqSize variable sets the size of the tiles in the generated map, while the RandomFillPercent variable sets the percentage of tiles that will be filled with a wall at the start of the generation process. The width and height variables set the dimensions of the generated map, and the seed variable can be used to set a specific seed for the random number generator used in the generation process. If UseRandomSeed is set to true, the seed will be generated based on the current time.

The script generates the map in the GenerateMap function by first initializing the map array with the specified dimensions, and then filling it with random values using the RandomFillMap function. The map is then smoothed using the SmoothMap function, which sets tiles to 1 or 0 based on the number of surrounding wall tiles. The SmoothMap function is called five times in a loop to increase the level of smoothing. Finally, the GenerateMesh function of the MeshGeneration component is called to create a mesh for the generated map.

The RandomFillMap function fills the map array with random values based on the given seed and RandomFillPercent. The function iterates over all tiles in the map, setting tiles along the edges of the map to 1 and all other tiles to 1 or 0 randomly based on the RandomFillPercent.

The SmoothMap function smooths the map by iterating over all tiles in the map and setting tiles to 1 or 0 based on the number of surrounding wall tiles. A tile is set to 1 if it has more than four surrounding wall tiles, 0 if it has less than four surrounding wall tiles, and its current value if it has exactly four surrounding wall tiles.

The GetSorroundWallCount function returns the number of walls surrounding a given grid coordinate. It iterates over all surrounding tiles and increments a counter if the tile contains a wall. If a surrounding tile is outside the boundaries of the map, it is considered a wall.

# MeshGeneration
This code is for generating a mesh from a 2D array of values representing terrain height. It uses the Marching Squares algorithm to create a mesh from a set of squares, where each square is determined by its corner values in the 2D array. The algorithm classifies each square into one of 16 possible configurations, and then generates triangles based on the configuration.

The class MeshGeneration is a MonoBehaviour class that has a public method GenerateMesh that takes in the 2D array of values and the square size, and generates a mesh from it. It also has a private method TriangulateSquare that generates the triangles for each square.

The class has two private fields vertices and triangles that are used to store the vertices and triangles of the mesh. The GenerateMesh method creates a new SquareGrid object, which is used to hold information about the squares in the grid, and then iterates through each square to triangulate it using the TriangulateSquare method.

The TriangulateSquare method uses a switch statement to determine which triangles to generate based on the configuration of the square. It calls the MeshFromPoints method to create triangles from the corner points of the square. The MeshFromPoints method creates new vertices if they don't already exist, and then adds the indices of those vertices to the triangles list to create a triangle.

# PlayerScript
This is a C# script for a player object in a game. The script defines the behavior of the player object, including its movement, collision detection, scoring, and game-ending conditions. Here are the main features of the script:

The script defines two serialized fields for the player speed and boundary. These fields can be edited in the Unity editor.
The script defines a private vector3 field for the player's position and two integer fields for the player's lives and points.
The script defines two public audio clip fields for the sound effects played when the player earns points or loses a life.
The Start() method sets the initial player position and lives and points to default values.
The Update() method is called every frame and updates the player's position based on user input, checks for the escape key to quit the game, and keeps the player within the defined boundary. It also calls the WinLose() method to check if the player has won or lost.
The AddPoints() method increments the player's score and plays the point sound effect.
The OnGUI() method displays the player's lives and score on the screen.
The TakeLife() method decrements the player's lives and plays the life sound effect.
The WinLose() method checks if the player has won or lost based on the number of lives and remaining blocks in the game. If the player has lost, it loads the Level_1 scene. If the player has won and is on the Level_1 scene, it loads the Level_2 scene. If the player has won and is on the Level_2 scene, it quits the game.

# TreeGen
This code defines a script for generating trees on a 3D map in a Unity game. The script generates a random number of trees based on the number of empty spaces (cells with a value of 0) in the map. The percentage of empty spaces is determined by the PercentCount() function, which counts the number of cells that are surrounded by other empty cells. The number of trees is then set to one-tenth of the count of such cells.

The TreeGenerate() function is responsible for generating the trees. For each tree, a random location is chosen within the map boundaries. If the chosen cell is already occupied by another tree, the loop will continue and try again. Otherwise, if the chosen cell is surrounded by empty cells, a new tree game object is instantiated at that location. The corresponding cell values in the map are then set to 1 to indicate that it is now occupied by a tree, and the surrounding cells are also set to 2 to indicate that they are now blocked and cannot be occupied by other trees.
