using UnityEngine;
using System.Collections;

//I copy / pasted the majority of this from the unity 2D example project. I didn't see the need to change what already works.

public class PlayerMovement : MonoBehaviour {
	//made this a property so can make readonly
	private bool facingRight = true;		// For determining which way the player is currently facing.
	public bool FacingRight {
		get { return facingRight; } }
	private bool jumpPressed = false;
	private bool jumping = false;
	 
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxWalkSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 50f;			// Amount of force added when the player jumps.
	public float maxJumpSpeed = 12f;

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

		if (Input.GetButton("Jump")) {
			jumpPressed = true;
		} else {
			jumpPressed = false;
			jumping = false;
		}

		if (grounded && jumpPressed) jumping = true;

		// Cache the horizontal input.
		horizontalInput = Input.GetAxis("Horizontal");
	}

	void FixedUpdate () {		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(horizontalInput * rigidbody2D.velocity.x < maxWalkSpeed) {
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * horizontalInput * moveForce);
		}
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxWalkSpeed) {
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxWalkSpeed, rigidbody2D.velocity.y);
		}

		// If the input is moving the player right and the player is facing left...
		if(horizontalInput > 0 && !facingRight) {
			// ... flip the player.
			Flip();		
		} else if(horizontalInput < 0 && facingRight) { // Otherwise if the input is moving the player left and the player is facing right...
			// ... flip the player.
			Flip();
		}
		
		// If the player is jumping
		if (jumping) {
			// Add a vertical force to the player.
			if (rigidbody2D.velocity.y <= maxJumpSpeed)
				rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			else jumping = false;
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(0, 0, 200, 20), "Velocity: " + rigidbody2D.velocity.ToString("F1"));
		GUI.Label(new Rect(0, 15, 200, 20), "Magnitude: " + rigidbody2D.velocity.magnitude.ToString("F1"));
		GUI.Label(new Rect(0, 30, 200, 20), "X: " + rigidbody2D.velocity.x.ToString("F1"));
		GUI.Label(new Rect(0, 45, 200, 20), "Y: " + rigidbody2D.velocity.y.ToString("F1"));
	}
	
	void Flip() {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1 to flip along axis
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}
}
