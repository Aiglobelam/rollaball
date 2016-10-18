using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// We will not use "forces" so we can use update rather than "FixedUpdate" 
	// ("FixedUpdate" which is Invoked just before performing any physics calculation)
	// Update is called once per frame.
	void Update () {
		// THis action also needs to be smooth, and independent, so multiply by Time.deltaTime
		// The time in seconds it took to complete the last frame.
		// WHY???? this multiply,,, hmmm my guess, it takes diffent amount of time to render a frame
		// if we allways rotate the same amount of degrees no matter what how long the last frame took to render
		// the rotation might look out of synch...
		this.transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
	}
}
