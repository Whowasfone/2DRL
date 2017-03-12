using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// initialize
// 		set reference to ai's transform to allow for movement
//		set the tile the ai spawns onto to 'impassable'
// Check to see if its the AI's turn each frame
// 		if (it is the AI's turn)
//			take action
//				move randomly to unoccupied tile
//					calculate goal tile
//					check goal tile's state --> Handled by Movement class
//				indicate action has been taken
//					pass turn back to the player
//
// In the case of multiple NPCs in the room - and thus, more AI to move in a given
// turn before the player would be allowed to move - a global counter might be
// a good idea to have. This counter can keep track of the amount of AI to move per turn,
// and would only pass the turn back to the player once all of the AI had decremented
// it (or a temp variable representing it), signifying that they had all taken moves.
//
// Changes needed:
// *Move tile state update logic from AIRoaming to Movement

public class AIRoaming : ObjectController 
{
	protected Vector3 prevPos;

	// Set the reference to the object's transform, and set the state of the tile the AI
	// spawns on to false.
	void Awake () 
	{
		obj = GetComponent<Transform> ();
		GridContainer.SetTile (GridContainer._grid.WorldToGrid(obj.position), false);
	}
	
	// LateUpdate is called once per frame, AFTER all Update() functions have been called.
	// This means that the AI movement will always wait until after the player has moved before
	// starting its turn. NOTE: In practice, this turned out not to work as I'd hoped, as the AI's
	// turn was still terribly out of sync. Another solution was implemented using coroutines, and
	// that seems to have worked as intended. This can likely be changed back into an Update()
	// function.
	void LateUpdate () 
	{
		// There is currently a bug where, if the player and AI move onto the same tile on the same
		// turn (which shouldn't happen if our coroutine is pausing long enough) and the AI gets
		// pushed off due to the collision of the 2D rigidbodies, the tile that the AI was previously
		// on will be set to false (because the AI did technically occupy it at the start of the
		// LateUpdate check) but won't be reset because the AI is no longer on that tile to reset it.
		//
		// This is probably because we don't set the player's tile to occupied, so the AI that checks
		// to see if a move is valid doesn't register the player's tile as occupied. To fix it, try
		// inserting the same tile state update logic into the player's movement.
		objPos = GridContainer._grid.WorldToGrid (obj.position);
		Vector3 newPos;
		int moveDir = Random.Range(0, 4);

		// Move up. First we reset the current tile (which will be the previous tile if movement is
		// successful) to true, then attempt movement, and then set the current position to false to
		// indicate the new tile is occupied and impassable. This should be set up in a way that
		// the tile updates are correct even if movement fails (such as when the AI attempts to move
		// into a wall), since newPos == objPos if the movement is not successful. 

		// The logic for setting the correct tile state can probably be moved to the Movement class
		// methods themselves to improve the readability of the controller.
		if (moveDir == 0 && GridConstructor.playersTurn == false)
		{
			GridContainer.SetTile (objPos, true);
			Movement.MoveUp ((objPos+Vector3.up), GridConstructor.rendererMaxY, obj);
			newPos = GridContainer._grid.WorldToGrid (obj.position);
			GridContainer.SetTile (newPos, false);
			GridConstructor.playersTurn = true;
		}
		// Move down
		if (moveDir == 1 && GridConstructor.playersTurn == false)
		{
			GridContainer.SetTile (objPos, true);
			Movement.MoveDown ((objPos+Vector3.down), 0.0f, obj);
			newPos = GridContainer._grid.WorldToGrid (obj.position);
			GridContainer.SetTile (newPos, false);
			GridConstructor.playersTurn = true;

		}
		// Move right
		if (moveDir == 2 && GridConstructor.playersTurn == false)
		{
			GridContainer.SetTile (objPos, true);
			Movement.MoveRight ((objPos+Vector3.right), GridConstructor.rendererMaxY, obj);
			newPos = GridContainer._grid.WorldToGrid (obj.position);
			GridContainer.SetTile (newPos, false);
			GridConstructor.playersTurn = true;

		}
		// Move left
		if (moveDir == 3 && GridConstructor.playersTurn == false)
		{
			GridContainer.SetTile (objPos, true);
			Movement.MoveLeft ((objPos+Vector3.left), 0.0f, obj);
			newPos = GridContainer._grid.WorldToGrid (obj.position);
			GridContainer.SetTile (newPos, false);
			GridConstructor.playersTurn = true;

		}
	}
}
