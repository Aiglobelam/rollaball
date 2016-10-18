using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//This is a reference to the player object (or it should be anyhow)
	//Making this public will let us connect some GameObject to this player variable in the Unity GUI...
	public GameObject player;

	//The diff between the camera object and the player oject in the form of a 3D Vector.
	private Vector3 offsetCameraPlayer;

	// Use this for initialization
	void Start () {
		//this.transform => Transform connectd to this object, that is the GameObject that this script is
		//currently attached to which in this case is the "camera"
		// The value assignd to offsetCameraPlayer is the ON GAME START diffrence between the two objects.
		offsetCameraPlayer = this.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	// Each frame before displayin what the camera can see, the camera is moved into a new position.
	// That is the camera is following allong the player object.

	/*
	void Update () {
		this.transform.position = player.transform.position + offsetCameraPlayer;
	}
	*/

	// For "Following" camears && procedural animations && gathering last known states
	// it is best to use "LateUpdate()". It runs every frame just like "Update()"
	// Guaranteed to be run after all items has been processed in "Update()"
	// So when we set the position for the camera we know for sure that the player has moved for that frame...
	// Hmmm sounds a bit weird but I trust for now.
	void LateUpdate () {
		this.transform.position = player.transform.position + offsetCameraPlayer;
	}

}
