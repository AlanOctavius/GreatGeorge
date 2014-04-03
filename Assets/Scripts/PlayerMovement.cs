using UnityEngine;
using System.Collections;

//I copy / pasted the majority of this from the unity 2D example project. I didn't see the need to change what already works.

public class PlayerMovement : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	
	 
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.

	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.

	private float horizontalInput = 0;

	void Start() {
		groundCheck = transform.Find("Ground Check");
	}

	void Update() {
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		int mask = 1 << LayerMask.NameToLayer("Ground");
		//Debug.Log(mask);
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, mask);
		
		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}

		// Cache the horizontal input.
		horizontalInput = Input.GetAxis("Horizontal");
	}

	void FixedUpdate ()
	{
		//I moved input to Update. getting input every frame instead of every fixed update should be more accurate / responsive. Not sure how important that is for this game though. whatever
		// Cache the horizontal input.
		//float h = Input.GetAxis("Horizontal");
		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(horizontalInput * rigidbody2D.velocity.x < maxSpeed) {
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * horizontalInput * moveForce);
		}
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed) {
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
		
		// If the input is moving the player right and the player is facing left...
		if(horizontalInput > 0 && !facingRight) {
			// ... flip the player.
			Flip();		
		} else if(horizontalInput < 0 && facingRight) { // Otherwise if the input is moving the player left and the player is facing right...
			// ... flip the player.
			Flip();
		}
		
		// If the player should jump...
		if(jump) {
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}
	
	
	void Flip () {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1 to flip along axis
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}
}
