using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
// boolean actionTaken --> whether or not the AI has acted this turn, resets every turn
// Check to see if its the AI's turn
// 		if (it is the AI's turn && it has not taken an action yet (actionTaken == 'false'))
//			take action
//				move randomly to unoccupied tile
//				increment global counter
//				indicate action has been taken (actionTaken == 'true')
//		if (it is not the AI's turn)
//			reset each AI's actionTaken to false
//
// In the case of multiple NPCs in the room - and thus, more AI to move in a given
// turn before the player would be allowed to move - a global counter might be
// a good idea to have. This counter can keep track of the amount of AI spawned,
// and would only pass the turn back to the player once all of the AI had decremented
// it (or a temp variable representing it), signifying that they had all taken moves.

public class AIRoaming : ObjectController 
{
	protected Vector3 prevPos;

	// Use this for initialization
	void Awake () 
	{
		obj = GetComponent<Transform> ();
	}
	
	// LateUpdate is called once per frame, AFTER all Update() functions have been called.
	// This means that the AI movement will always wait until after the player has moved before
	// starting its turn.
	void LateUpdate () 
	{
		// prevPos will be used to hold the value of the last location this AI object occupied.
		// Eventually, we will use this to handle updating the position of the AI in _tiles,
		// but I have yet to find an elegant solution for this.
		objPos = GridContainer._grid.WorldToGrid (obj.position);
		prevPos = objPos;
		int moveDir = Random.Range(0, 4);

		// Move up
		if (moveDir == 0 && GridConstructor.playersTurn == false)
		{
			Movement.MoveUp ((objPos+Vector3.up), GridConstructor.rendererMaxY, obj);
			GridConstructor.playersTurn = true;
		}
		// Move down
		if (moveDir == 1 && GridConstructor.playersTurn == false)
		{
			Movement.MoveDown ((objPos+Vector3.down), 0.0f, obj);
			GridConstructor.playersTurn = true;

		}
		// Move right
		if (moveDir == 2 && GridConstructor.playersTurn == false)
		{
			Movement.MoveRight ((objPos+Vector3.right), GridConstructor.rendererMaxY, obj);
			GridConstructor.playersTurn = true;

		}
		// Move left
		if (moveDir == 3 && GridConstructor.playersTurn == false)
		{
			Movement.MoveLeft ((objPos+Vector3.left), 0.0f, obj);
			GridConstructor.playersTurn = true;

		}
	}
}
