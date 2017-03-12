using System.Collections;
using System.Collections.Generic;
using GridFramework.Grids;
using GridFramework.Renderers.Rectangular;
using UnityEngine;

// This is the script that handles everything having to do with the player. Right now it detects input from the
// player each frame, and passes that input data to the Movement script to physically move the player's character
// in the game world.
//
// Changes needed:
// *PlayerController needs to be re-implemented as a child of a parent ObjectController class (waiting for the
// 	ObjectController class to be created)
// *Update the variable keeping track of the turn state after a player move to allow the AI to take their move (waiting
// 	on the basic AI script to be finished before implementing). Not sure where that turn state variable will be
//  initialized and held yet.
//
// Changelog (started 3/8/2017):
// *3/11/2017
//		-Added a line to the Awake() function that sets the player's initial spawn tile to false to indicate it is
//		 occupied.
// *3/10/2017
//		-Changed PlayerController to inherit from a more abstract ObjectController parent class. The goal is to have
//		 basic information (such as health and stats) to be defined for every game object (which in this case means
//		 any entity that can be interacted with on the board. This might need to be changed again later, but it's
//		 working for now, and I'm sure we'll have some generalized methods that will be useful on all objects at a
//		 later point in development.
// *3/9/2017
// 		-Changed "player"'s type from a PlayerController to a Transform since the only component of "player" we're
//		 interested in is its transform, for movement purposes.
//		-Removed the "goal" Vector3; it was a middleman variable that held the pre-check coordinates of the tile the
//		 player wanted to move onto when input was detected. Instead of making a variable to hold the coordinates and
//		 then immediately pass those coords to the movement method, it made more sense to just pass the playerTile
//		 Vector3 (along with an offset corresponding to the type of movement) to the movement methods directly.
//		-Moved state machine variable update to Movement class methods dealing specifically with player movement
//		 (MovePlayerUp, MovePlayerDown, MovePlayerRight, MovePlayerLeft)
// *3/8/2017
//		-Moved the logic that changes the player's position to the Movement script, instead of having the controller
//		 define movement separately for the player. This should allow for all movement of game objects (including the 
//		 player) to be handled by the Movement class, which will help the readability of the PlayerController going 
//		 forward.
//

public class PlayerController : ObjectController 
{
	// Awake will run before any other methods execute, so we'll initialize any variables inside here that need
	// to be initialized when the player is drawn to the screen. Currently, we set the reference to the player's
	// transform, and then update our tile array to indicate that the player's tile is impassable to other objects.
	void Awake () 
	{
		obj = GetComponent<Transform> ();
		GridContainer.SetTile (GridContainer._grid.WorldToGrid(obj.position), false);
	}

	void Update ()
	{
		// Convert the player's current worldspace coords into grid coords
		objPos = GridContainer._grid.WorldToGrid (obj.position);

		// MOVEMENT AND INPUT DETECTION
		//-----------------------------------
		// Move the player along the grid in discrete steps, instead of smoothly. The PlayerMovement class handles all of
		// the logic pertaining to moving a game object in the world, including checking if the desired move is valid.
		// The goal variable will be passed into the various movement methods corresponding to each different keystroke,
		// and will represent the position we want to move to. Each method will take a second parameter defining the
		// boundaries of each type of move, and a third that indicates what object to move - which is the player in
		// this case.
		if (Input.GetKeyDown("up") && GridConstructor.playersTurn)
		{
			PlayerMovement.MoveUp ((objPos+Vector3.up), GridConstructor.rendererMaxY, obj);
		}
		if (Input.GetKeyDown("down") && GridConstructor.playersTurn)
		{
			PlayerMovement.MoveDown ((objPos+Vector3.down), 0.0f, obj);
		}
		if (Input.GetKeyDown("right") && GridConstructor.playersTurn)
		{
			PlayerMovement.MoveRight ((objPos+Vector3.right), GridConstructor.rendererMaxX, obj);
		}
		if (Input.GetKeyDown("left") && GridConstructor.playersTurn)
		{
			PlayerMovement.MoveLeft ((objPos+Vector3.left), 0.0f, obj);
		}
	}

	// A coroutine that prevents the player from moving again until the AI has taken its turn.
}
