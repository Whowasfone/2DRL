using UnityEngine;

public class BlockedTile : MonoBehaviour {

	// Update the grid to indicate that this tile is impassable.
	void Awake ()
	{
		GridContainer.SetTile (transform.position, false);
	}
}
