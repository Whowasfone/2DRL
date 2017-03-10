using System.Collections;
using System.Collections.Generic;
using GridFramework.Grids;
using GridFramework.Renderers.Rectangular;
using UnityEngine;

// Static class used to hold the reference to our grid.
public static class GridContainer {

	// Public variables
	// 2D array of tiles that comprise our grid. There are 2 states to track for each space: Passable (True) and Impassable (False).
	public static bool[,] _tiles;

	// The grid object itself
	public static RectGrid _grid;

	// Public methods

	// Initialize will set up our grid matrix and populate it initially with Passable (True) tiles. The renderer is used to
	// determine the size of the matrix, which will track playable space only.
	public static void Initialize(RectGrid grid, Parallelepiped renderer)
	{
		// Set up the reference to our RectGrid
		_grid = grid;

		// Get the # of rows and columns based on our renderer's range
		var rows = Mathf.FloorToInt(renderer.To.x);
		var cols = Mathf.FloorToInt(renderer.To.y);

		_tiles = new bool[rows, cols];

		// Iterate over the array, setting each tile's initial state to True/Passable
		for (int currRow = 0; currRow < rows; currRow++)
		{
			for (int currCol = 0; currCol < cols; currCol++)
			{
				_tiles [currRow, currCol] = true;
			}
		}
	}

	// SetTile is used to change the states of a tile on the map. The tile param is the position of the tile in
	// its worldspace coordinates, and the status param is the state (True/False) we want to set it to. First, we
	// convert the tile's worldspace coordinates to grid coordinates, then cast the x and y both as floored integers
	// to set that tile's state to impassable in our 2D array of boolean tile states.
	public static void SetTile(Vector3 tile, bool status)
	{
		tile = _grid.WorldToGrid (tile);
		_tiles [Mathf.FloorToInt (tile.x), Mathf.FloorToInt (tile.y)] = status;
	}

	// CheckTile takes worldspace coords and then returns the state of the tile indicated by the params.
	public static bool CheckTile(Vector3 tile)
	{
		return _tiles [Mathf.FloorToInt (tile.x), Mathf.FloorToInt (tile.y)];
	}
}
