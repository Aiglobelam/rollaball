using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// public vars will show up in the "inspector" for the object as an editable property.
	// Then we can make all our changes to this var in the editor instead of in the script.
	// This saves us from recompiling the script all the time we want to update this value.
	public float speed;
	public float jumpforce;
	private float distanceToGround;

	private Rigidbody rb;
	private Collider playerCollider;

	bool isGrounded(){
		return Physics.Raycast (this.transform.position, -Vector3.up, distanceToGround + 0, 1);
	}

	// Start is called in the first frame of which this very Script (PlayerController.cs) is ACTIVE
	// That is the very first FRAME of the game.
	void Start(){
		// This will find and return a refference to a RigidBody if it exist one on the Game Object
		// That this very Script (PlayerController.cs) is attached to.
		rb = GetComponent<Rigidbody> ();
		playerCollider = GetComponent<Collider> ();
		distanceToGround = playerCollider.bounds.extents.y;
		print ("distanceToGround: " + distanceToGround);
		print (transform.position);
		print (isGrounded ());
	}

	// We want to check every frame for player input
	// And then we want to apply that input on every frame to the game object (Player) as movement
	// We have 2 choises for where to do this "Update" and "FixedUpdate"

	// Update is called before rendering a frame, and this is where our gamecode will go
	void Update(){
		//print ("x:"+rb.velocity.x+", y:"+rb.velocity.y+" z:"+rb.velocity.z);

		if (Input.GetKeyDown ("space")) {
			print ("space key was pressed");
			print ("isGrounded: " + isGrounded());
		}

		if (Input.GetKeyDown("space") && isGrounded()) {
			rb.AddForce(Vector3.up * jumpforce);

		}
	}

	// Invoked just before performing any physics calculation, this is where our physics code will go
	// We will move the ball by applying forces to the "RigidBody"
	void FixedUpdate(){

		if (isGrounded()) {
			// The Horizontal and Vertical "axis" is controlled by the arrow keys on keyboard.
			// Our "Player" Object (the ball) has a "Rigidbody" and interacts with the PhysEngine
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			// We will use these inputs to add forces to the "RigidBody" and move the Player Object in the Scene

			// There exist diffrent ways of accessing onother Component on a Game object "Player" in this case.
			// We use the rb reference we created in Start()
			Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

			rb.AddForce(movement * speed);
		}
	
	}

	/** https://unity3d.com/learn/tutorials/projects/roll-ball-tutorial/collecting-pick-objects?playlist=17141
	 * How do collisions work in Unitys Physics Engine?
	 * Two Collider volumes are not allowed to overlap.
	 * When PE detects that two or more Collider volumes overlap, PE will analyse the objects speed rotaion and shape.
	 * And calculate the collision. One of the major factors in this calculation is wheter Colliders are STATIC of DYNAMIC.
	 * 
	 * => Static Colliders:: are usually none moving parts of your scene. Like walls, floor etc.
	 * 
	 * => Dynamic Colliders:: are things that move, like the player.
	 * 
	 * When calculating a collision the static gemoetry will not be affected by the collision.
	 * But the Dynamic objects will be affected.
	 * => Our player sphere will bounce of the walls/pickup objects.
	 * The PE can ALLOW overlapp / penetraions of Collider volumes, when it allows this it still calculate the collission
	 * of the Collider volumes AND keep tracks of the Colliders overlap, but it does not physically act on the overlapping
	 * objects. => It does not cause a collission. 
	 * We activate this behaviour by making our objects into => Triggers aka TriggerColliders 
	 * When we make our colliders in to a trigger (or trigger collider)
	 * => We can detect the contact with that trigger through the OnTrigger event messages.
	 * => We are below using OnTriggerEnter rather than OnCollisionEnter, that is we need to change our Colider volumes
	 * into Trigger volumes.
	 * 
	 * Unity calculates all the volumes of all the static colliders in a scene and holds this information in a cache.
	 * This makes sense as static colliders shouldn't move, and this saves recalculating this information every frame.
	 * 
	 * => Our mistake is by rotating our cubes. (which have static colliders, because the object is not moving???)
	 *    Any time we move, rotate, or scale a static collider, Unity will recalculate all the static colliders again
	 *    and update the static collider cache. To recalculate the cache takes resources.
	 * 
	 * => dynamic colliders::  We can move, rotate or scale dynamic colliders as often as we want and Unity won't 
	 *    recache any collider volumes. Unity expects us to move colliders.
	 * 
	 * Solution: We simply need to indicate to Unity which colliders are dynamic before we move them.
	 *           We do this by using the rigid body component. Any game object with a collider 
	 *           and a (Phisics) rigid body is considered dynamic. Objects without rigid body is concidered static collider.
	 * 
	 * Problem: Our "Pick Up" object does not have a RigidBody.
	 * 
	 */

	// OnTriggerEnter will be called by Unity when our Player GameObject first touches a TriggerCollider
	// We are as a param given a reference to the TriggerCollider that we (Player) have touchd.
	// This code will => be called EVERY TIME we touch a TriggerCollider.
	void OnTriggerEnter(Collider other) {
		//Destroy will remove the component and its children completely from the Scene.
		//Destroy(other.gameObject);

		//But we just want to deactivate it,
		// TAG "Pick up" has to be defined un Unity, DUH
		if(other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.SetActive(false);
		}
	}
}
