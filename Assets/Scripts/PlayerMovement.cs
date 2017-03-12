using System.Collections;
using System.Collections.Generic;
using MovementEffects;
using UnityEngine;

// The PlayerMovement movement methods function a little differently from its parent class's methods. These
// methods are pulling "double duty" (which may or may not be a good idea) by both updating the player's
// position as well as changing the turn state from "player" to "AI". This is done by running the
// _PassTurnControl coroutine, which will "pause" the execution of the script until the playersTurn variable
// has been set back to true by the AI. We're using Trinary Software's More Effective Coroutines asset to
// implement our coroutines, instead of Unity's built-in coroutines that must be ran from MonoBehaviour-inherited
// scripts.
public class PlayerMovement : Movement 
{
	public static void MoveUp(Vector3 goal, float yMax, Transform controller)
	{
		if (goal.y < yMax)
		{
			if (GridContainer.CheckTile (goal))
			{
				GridContainer.SetTile (controller.position, true);
				controller.position = GridContainer._grid.GridToWorld (goal);
				GridContainer.SetTile (controller.position, false);
				GridConstructor.playersTurn = false;
				Timing.RunCoroutine (_PassTurnControl ());
			}
		}
	}
	
	public static void MoveDown(Vector3 goal, float yMin, Transform controller)
	{
		if (goal.y > yMin)
		{
			if (GridContainer.CheckTile (goal))
			{
				GridContainer.SetTile (controller.position, true);
				controller.position = GridContainer._grid.GridToWorld (goal);
				GridContainer.SetTile (controller.position, false);
				GridConstructor.playersTurn = false;
				Timing.RunCoroutine (_PassTurnControl ());
			}
		}
	}

	public static void MoveRight(Vector3 goal, float xMax, Transform controller)
	{
		if (goal.x < xMax)
		{
			if (GridContainer.CheckTile (goal))
			{
				GridContainer.SetTile (controller.position, true);
				controller.position = GridContainer._grid.GridToWorld (goal);
				GridContainer.SetTile (controller.position, false);
				GridConstructor.playersTurn = false;
				Timing.RunCoroutine (_PassTurnControl ());
			}
		}
	}

	public static void MoveLeft(Vector3 goal, float xMin, Transform controller)
	{
		if (goal.x > xMin)
		{
			if (GridContainer.CheckTile (goal))
			{
				GridContainer.SetTile (controller.position, true);
				controller.position = GridContainer._grid.GridToWorld (goal);
				GridContainer.SetTile (controller.position, false);
				GridConstructor.playersTurn = false;
				Timing.RunCoroutine (_PassTurnControl ());
			}
		}
	}

	// Coroutine that will pause the player's movement functions, suspending further
	// action until the following frame, by which time the AI's turn will have been taken.
	static IEnumerator<float> _PassTurnControl()
	{
		while (GridConstructor.playersTurn == false)
		{
			yield return 0.0f;
		}
	}
}
