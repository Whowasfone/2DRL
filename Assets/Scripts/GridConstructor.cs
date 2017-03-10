using System.Collections;
using System.Collections.Generic;
using GridFramework.Grids;
using GridFramework.Renderers.Rectangular;
using UnityEngine;

// In order to use the GridConstructor script, we need to have RectGrid and Parallelepiped scripts to construct it from. RequiredComponent
// will automatically add the specificed components to the GameObject that GridConstructor is attached to, to prevent accidental
// missing script dependencies.
[RequireComponent(typeof (RectGrid))]
[RequireComponent(typeof (Parallelepiped))]

// Changes needed:
// *Change the typing for the playersTurn variable. We might also be able to generalize this into a catchall variable that controls the
// game's state machine. This would make switching between menu states and back to the game state MUCH easier.
// 		-State machine compartmentalization reference: https://gamedevelopment.tutsplus.com/articles/how-to-build-a-jrpg-a-primer-for-game-developers--gamedev-6676
public class GridConstructor : MonoBehaviour 
{
	// We need to know the bounds of our grid array when dealing with the movement of objects on the grid, so we'll reference these static
	// variables in our movement script.
	public static float rendererMaxX;
	public static float rendererMaxY;
	public static bool playersTurn = true;
	public int numWalls;
	public GameObject player;
	public GameObject[] enemies;
	public GameObject[] wallTiles;
	public GameObject[] floorTiles;

	// Private variable that will be used to set our Grid object as a parent to the terrain it generates. This will allow us to collapse
	// all of our generated terrain into our Grid so that we don't clutter up the inspector on runtime.
	private Transform gridHierarchyHandler;

	// We use the Awake() method to initialize the grid before any Start() methods are ran.
	void Awake ()
	{
		var grid = GetComponent<RectGrid> ();
		var para = GetComponent<Parallelepiped> ();
		rendererMaxX = para.To.x;
		rendererMaxY = para.To.y;
		gridHierarchyHandler = GetComponent<Transform>();

		GridContainer.Initialize (grid, para);
	}

	// After Awake runs, we'll use Start to fill the grid with different tiles, including our player, and eventually enemies and other
	// objects.
	void Start()
	{
		GenerateWalls();
		GenerateFloors();
		GenerateEnemies();
		GeneratePlayer();
	}

	// Generates walls at random positions in the grid. The 0.5 offsets in their X- and Y-coordinate calculations
	// are so they align with the grid nicely.
	void GenerateWalls()
	{
		float randX;
		float randY;
		bool validPos;
		Vector3 spawnPos;

		// First we'll generate the boundary walls.
		for (int x = 0; x < rendererMaxX; x++)
		{
			for (int y = 0; y < rendererMaxY; y++)
			{
				if (x == 0 || x == rendererMaxX-1 || y == 0 || y == rendererMaxY-1)
				{
					GameObject toInstantiate = wallTiles [Random.Range (0, wallTiles.Length)];
					GameObject wall = GameObject.Instantiate (toInstantiate, new Vector3 ((x+0.5f), (y+0.5f), 0.0f), Quaternion.identity) as GameObject;
					wall.transform.SetParent (gridHierarchyHandler);
				}
			}
		}

		// Next we'll generate a number of non-boundary walls equal to numWalls
		for (int numGenerated = 0; numGenerated < numWalls; numGenerated++)
		{
			do
			{
				randX = Mathf.FloorToInt (Random.Range (1.0f, rendererMaxX-1)) + 0.5f;
				randY = Mathf.FloorToInt (Random.Range (1.0f, rendererMaxY-1)) + 0.5f;
				spawnPos = new Vector3 (randX, randY, 0.0f);
				validPos = GridContainer.CheckTile (spawnPos);
			} while(!validPos);

			// Instantiate a wall and set its parent to our gridHierarchyHandler Transform. This allows us to
			// collapse all of our cloned wall instances under the Grid GameObject in the inspector.
			GameObject toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
			GameObject wall = GameObject.Instantiate (toInstantiate, spawnPos, Quaternion.identity) as GameObject;
			wall.transform.SetParent (gridHierarchyHandler);
		}
	}

	void GenerateFloors()
	{
		for (int x = 1; x < rendererMaxX-1; x++)
		{
			for (int y = 1; y < rendererMaxY - 1; y++)
			{
				GameObject toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
				GameObject floor = GameObject.Instantiate (toInstantiate, new Vector3 ((x + 0.5f), (y + 0.5f), 0.0f), Quaternion.identity) as GameObject;
				floor.transform.SetParent (gridHierarchyHandler);
			}
		}
	}

	void GenerateEnemies()
	{
		float randX;
		float randY;
		bool validPos;
		Vector3 spawnPos;

		do
		{
			randX = Mathf.FloorToInt (Random.Range (1.0f, rendererMaxX-1)) + 0.5f;
			randY = Mathf.FloorToInt (Random.Range (1.0f, rendererMaxY-1)) + 0.5f;
			spawnPos = new Vector3 (randX, randY, 0.0f);
			validPos = GridContainer.CheckTile (spawnPos);
		} while(validPos == false);

		GameObject enemyInstance = GameObject.Instantiate (enemies[0], spawnPos, Quaternion.identity) as GameObject;
	}

	// Generate the player after checking the position they should be generated at.
	void GeneratePlayer()
	{
		float randX;
		float randY;
		bool validPos;
		Vector3 spawnPos;

		do
		{
			randX = Mathf.FloorToInt (Random.Range (1.0f, rendererMaxX-1)) + 0.5f;
			randY = Mathf.FloorToInt (Random.Range (1.0f, rendererMaxY-1)) + 0.5f;
			spawnPos = new Vector3 (randX, randY, 0.0f);
			validPos = GridContainer.CheckTile (spawnPos);
		} while(validPos == false);

		GameObject playerInstance = GameObject.Instantiate (player, spawnPos, Quaternion.identity) as GameObject;
	}
}
