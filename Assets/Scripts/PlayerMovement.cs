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

	//DEBUG serialzed for testing
	[SerializeField] private float moveForce = 365f;			// Amount of force added to move the player left and right.
	[SerializeField] private float maxWalkSpeed = 5f;				// The fastest the player can travel in the x axis.
	[SerializeField] private float jumpForce = 50f;			// Amount of force added when the player jumps.
	[SerializeField] private float initialJumpForce = 250f;
	[SerializeField] private float maxJumpSpeed = 12f;

	private bool firstFrameJump = false;

	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.

	private float horizontalInput = 0;

	private Vector3 cursorPos;
	private Vector3 cursorInWorld;

	private Animator anim;

	void Start() {
		groundCheck = transform.Find("Ground Check");
		anim = GetComponent<Animator>() as Animator;
	}

	void Update() {
		if (!GameManagerScript.Paused) {
			// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
			int mask = 1 << LayerMask.NameToLayer("Ground");
			//Debug.Log(mask);
			grounded = Physics2D.Linecast(transform.position, groundCheck.position, mask);

			cursorPos = Input.mousePosition;
			cursorInWorld = Camera.main.ScreenToWorldPoint(cursorPos);
			if (facingRight) {
				if (cursorInWorld.x < transform.position.x) { Flip(); }
			} else {
				if (cursorInWorld.x > transform.position.x) { Flip(); }
			}

			if (Input.GetButton("Jump")) {
				jumpPressed = true;
				if (firstFrameJump) firstFrameJump = false;
			} else {
				jumpPressed = false;
				jumping = false;
			}

			if (Input.GetButtonDown("Jump")) {
				firstFrameJump = true;
			}

			if (grounded && jumpPressed) jumping = true;

			// Cache the horizontal input.
			horizontalInput = Input.GetAxis("Horizontal");
		}
	}

	void FixedUpdate () {		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		/*
		 * as is, careless adding force can push player against floating plaform; player can "float" in air by pressing self against edge of floating platform.
		 * if use grounded check, this goes away, but this introducs two more problems.
		 *  1) player loses horizontal control while in air (trajectory must be decided before jumping)
		 *  2) there are times that the circle collider says player is on ground (player doesn't fall) but ground check / line cast does not. this causes player to be stuck in place at the edge of a platform.
		 *  a different ground check mechanism could relieve second problem.
		 */
		if(horizontalInput * rigidbody2D.velocity.x < maxWalkSpeed) {
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * horizontalInput * moveForce);
		}
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxWalkSpeed) {
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxWalkSpeed, rigidbody2D.velocity.y);
		}
		
		// If the player is jumping
		if (jumping) {
			// Add a vertical force to the player.
			if (rigidbody2D.velocity.y <= maxJumpSpeed) {
				if (firstFrameJump) {
					rigidbody2D.AddForce(new Vector2(0f, initialJumpForce));
					print("initial frame");
				} else {
					rigidbody2D.AddForce(new Vector2(0f, jumpForce));
				}
			} else jumping = false;
		}

		anim.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
		anim.SetBool("Jump", !grounded);
	}

	/*void OnGUI() {
		GUI.Label(new Rect(0, 0, 200, 20), "Velocity: " + rigidbody2D.velocity.ToString("F1"));
		GUI.Label(new Rect(0, 15, 200, 20), "Magnitude: " + rigidbody2D.velocity.magnitude.ToString("F1"));
		GUI.Label(new Rect(0, 30, 200, 20), "X: " + rigidbody2D.velocity.x.ToString("F1"));
		GUI.Label(new Rect(0, 45, 200, 20), "Y: " + rigidbody2D.velocity.y.ToString("F1"));
	}*/
	
	void Flip() {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1 to flip along axis
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}
}
