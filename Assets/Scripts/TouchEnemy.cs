﻿using UnityEngine;
using System.Collections;

public class TouchEnemy : Character {


	// Checks
	public bool aheadContact = false;
	public bool aheadGround = false;
	public bool grounded = false;			// Whether or not the player is grounded.
	private Transform aheadContactCheck;
	private Transform aheadGroundCheck;
	private Transform groundCheck;			// A position marking where to check if the player is grounded.

	protected int pointValue = 10;

	public bool debugAheadFloor = false;

	//Movement
	public float moveForce = 50f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 1f;				// The fastest the player can travel in the x axis.

	public int movingDirection = 1;	// Bool to show direction
	public bool changingDirection = false; //bool to show if direction changed last frame ie hit a wall


	//Useful transform
	private Vector3 invertYVec = new Vector3(-1,1,1);

	// Use this for initialization
	void Start () {

		groundCheck = transform.FindChild ("Ground Check");
		aheadContactCheck = transform.FindChild ("Ahead Check");
		aheadGroundCheck = transform.FindChild ("Ahead Ground Check");
		ExtraStart ();
	}
	
	// Update is called once per frame
	void Update () {

		if (changingDirection) {
			aheadGroundCheck.localPosition =  Vector3.Scale( invertYVec , aheadGroundCheck.localPosition );
			aheadContactCheck.localPosition =  Vector3.Scale( invertYVec , aheadContactCheck.localPosition );
			movingDirection *= -1;
			changingDirection = false;
		}
	
		int mask = 1 << LayerMask.NameToLayer("Ground");
		// if something is in the way or there is no floor set the change direction flag
		aheadContact = Physics2D.Linecast (transform.position, aheadContactCheck.position,mask);
		grounded = Physics2D.Linecast (transform.position, groundCheck.position,mask);
		// Ahead ground fails if there is nothing ahead
		// Place back in so hostile do not walk off ledge
		//aheadGround = !Physics2D.Linecast (transform.position, aheadGroundCheck.position,mask);

		if (aheadGround) {
			debugAheadFloor = true;
				}

		if ((aheadGround || aheadContact)&&grounded) {
			changingDirection = true;
				}

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(Mathf.Abs(movingDirection * rigidbody2D.velocity.x )< maxSpeed && grounded) {
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * movingDirection * moveForce);
		}


	}


	void OnCollisionEnter2D(Collision2D collision){

		if (collision.gameObject.tag == "Player") {
			PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
			player.TakeDamage(1);
			Debug.Log("playerhit");
			changingDirection = true;
			}
		if (collision.gameObject.tag == "Hostile"){
			changingDirection = true;
			}
		}

	protected override void Die(){
		GameManagerScript.IncreaseScore (pointValue);
		Destroy(gameObject);

		}
	protected virtual void ExtraStart(){
	}


}