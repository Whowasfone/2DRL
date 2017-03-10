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

public class PlayerController : MonoBehaviour 
{

	private Vector3 playerTile;
	private Transform player;

	// Awake will run before any other methods execute, so we'll initialize any variables inside here that need
	// to be initialized when the player is drawn to the screen.
	void Awake () 
	{
		player = GetComponent<Transform> ();
	}

	void Update ()
	{
		// Convert the player's current worldspace coords into grid coords
		playerTile = GridContainer._grid.WorldToGrid (player.position);

		// Move the player along the grid in discrete steps, instead of smoothly. The Movement class handles all of
		// the logic pertaining to moving a game object in the world, including checking if the desired move is valid.
		// The goal variable will be passed into the various movement methods corresponding to each different keystroke,
		// and will represent the position we want to move to. Each method will take a second parameter defining the
		// boundaries of each type of move, and a third that indicates what object to move - which is the player in
		// this case.
		if (Input.GetKeyDown("up") && GridConstructor.playersTurn)
		{
			Movement.MovePlayerUp ((playerTile+Vector3.up), GridConstructor.rendererMaxY, player);
		}
		if (Input.GetKeyDown("down") && GridConstructor.playersTurn)
		{
			Movement.MovePlayerDown ((playerTile+Vector3.down), 0.0f, player);
		}
		if (Input.GetKeyDown("right") && GridConstructor.playersTurn)
		{
			Movement.MovePlayerRight ((playerTile+Vector3.right), GridConstructor.rendererMaxX, player);
		}
		if (Input.GetKeyDown("left") && GridConstructor.playersTurn)
		{
			Movement.MovePlayerLeft ((playerTile+Vector3.left), 0.0f, player);
		}
	}
}
