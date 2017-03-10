using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic class that handles the movement of objects on our grid. The methods of the Movement class have been refactored
// so that they take Transforms as parameters representing game objects, which allows us to use them to move any object
// - including AI objects - instead of just the player.
//
// Changes Needed:
// *
//
// Changelog (started 3/9/2017):
// *3/9/2017
//		-Refactored orthogonal movement methods to take more generic controller parameters in order to allow any gameobject 
//		to be moved using this class.
//		-Added yMin and xMin parameters to MoveDown and MoveLeft so that we can restrict movement to certain sub-sections
//		 of the grid if we want to.
//

public class Movement
{
	// Attemps to move an object up, after checking if the desired coordinate pair indicated by goal is valid. The yMax
	// parameter defines the highest y-coordinate that a given object can move to, and the controller parameter is
	// a reference to the object that is calling MoveUp and is used to update that object's position.
	public static void MoveUp(Vector3 goal, float yMax, Transform controller) 
	{
		if (goal.y < yMax)
		{
			if (GridContainer.CheckTile (goal))
			{
				controller.position = GridContainer._grid.GridToWorld (goal);
			}
		}
	}

	// More specific player movement function
	public static void MovePlayerUp(Vector3 goal, float yMax, Transform controller)
	{
		if (goal.y < yMax)
		{
			if (GridContainer.CheckTile (goal))
			{
				controller.position = GridContainer._grid.GridToWorld (goal);
				GridConstructor.playersTurn = false;
			}
		}
	}

	// Attempts to move an object down, after checking if the desired coordinate pair indicated by goal is valid. The yMin
	// parameter defines the lowest y-coordinate that a given object can move to, and the controller parameter is
	// a reference to the object that is calling MoveDown and is used to update that object's position.
	public static void MoveDown(Vector3 goal, float yMin, Transform controller)
	{
		if (goal.y > yMin)
		{
			if (GridContainer.CheckTile (goal))
			{
				controller.position = GridContainer._grid.GridToWorld (goal);
			}
		}
	}

	// More specific player movement function
	public static void MovePlayerDown(Vector3 goal, float yMin, Transform controller)
	{
		if (goal.y > yMin)
		{
			if (GridContainer.CheckTile (goal))
			{
				controller.position = GridContainer._grid.GridToWorld (goal);
				GridConstructor.playersTurn = false;
			}
		}
	}

	// Attempts to move an object right, after checking if the desired coordinate pair indicated by goal is valid. The xMax
	// parameter defines the highest x-coordinate that a given object can move to, and the controller parameter is
	// a reference to the object that is calling MoveRight and is used to update that object's position.
	public static void MoveRight(Vector3 goal, float xMax, Transform controller)
	{
		if (goal.x < xMax)
		{
			if (GridContainer.CheckTile (goal))
			{
				controller.position = GridContainer._grid.GridToWorld (goal);
			}
		}
	}

	// More specific player movement function
	public static void MovePlayerRight(Vector3 goal, float xMax, Transform controller)
	{
		if (goal.x < xMax)
		{
			if (GridContainer.CheckTile (goal))
			{
				controller.position = GridContainer._grid.GridToWorld (goal);
				GridConstructor.playersTurn = false;
			}
		}
	}

	// Attempts to move an object left, after checking if the desired coordinate pair indicated by goal is valid. The xMin
	// defines the lowest x-coordinate that a given object can move to, and the controller parameter is
	// a reference to the object that is calling MoveLeft and is used to update that object's position.
	public static void MoveLeft(Vector3 goal, float xMin, Transform controller)
	{
		if (goal.x > xMin)
		{
			if (GridContainer.CheckTile (goal))
			{
				controller.position = GridContainer._grid.GridToWorld (goal);
			}
		}
	}

	// More specific player movement function
	public static void MovePlayerLeft(Vector3 goal, float xMin, Transform controller)
	{
		if (goal.x > xMin)
		{
			if (GridContainer.CheckTile (goal))
			{
				controller.position = GridContainer._grid.GridToWorld (goal);
				GridConstructor.playersTurn = false;
			}
		}
	}
}
