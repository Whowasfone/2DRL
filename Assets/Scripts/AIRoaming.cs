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

public class AIRoaming : MonoBehaviour 
{
	private Transform aiObject;
	private Vector3 aiPos;

	// Use this for initialization
	void Awake () 
	{
		aiObject = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		aiPos = GridContainer._grid.WorldToGrid (aiObject.position);
		int moveDir = Random.Range(0, 4);

		// Move up
		if (moveDir == 0 && GridConstructor.playersTurn == false)
		{
			Movement.MoveUp ((aiPos+Vector3.up), GridConstructor.rendererMaxY, aiObject);
			GridConstructor.playersTurn = true;
		}
		// Move down
		if (moveDir == 1 && GridConstructor.playersTurn == false)
		{
			Movement.MoveDown ((aiPos+Vector3.down), 0.0f, aiObject);
			GridConstructor.playersTurn = true;
		}
		// Move right
		if (moveDir == 2 && GridConstructor.playersTurn == false)
		{
			Movement.MoveRight ((aiPos+Vector3.right), GridConstructor.rendererMaxY, aiObject);
			GridConstructor.playersTurn = true;
		}
		// Move left
		if (moveDir == 3 && GridConstructor.playersTurn == false)
		{
			Movement.MoveLeft ((aiPos+Vector3.left), 0.0f, aiObject);
			GridConstructor.playersTurn = true;
		}
	}
}
