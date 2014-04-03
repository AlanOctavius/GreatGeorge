using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}

	//TODO fill in mechanics of hitting enemies
}
