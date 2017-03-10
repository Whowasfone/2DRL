using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public Transform player;

	// Centers the camera on the player's position every frame.
	void Update () 
	{
		transform.position = new Vector3 (player.position.x, player.position.y, -10.0f);
	}
}
