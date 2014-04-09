using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	[SerializeField]
	private Rigidbody2D bullet;  //assigned in inspector

	//DEBUG serialized both for testing
	[SerializeField]
	private float speed = 20f;   // The speed the bullet will fire at.
	[SerializeField]
	private bool shootAtCursor = false;

	private PlayerMovement playerMoveScript;

	private Quaternion bulletRotation; //bullet is round so its rotation doesn't matter. but if that changes, we do have to change its orientation as it's done below
	private Vector2 bulletVelocity;

	private Vector3 cursorPos;
	private Vector3 cursorInWorld;

	void Start() {
		playerMoveScript = transform.root.GetComponent<PlayerMovement>();
	}

	void Update() {
		if (!shootAtCursor) {
			if (Input.GetButtonDown("Fire1") && !GameManagerScript.Paused) { //better way of checking pause?
				// If the player is facing right...
				if(playerMoveScript.FacingRight) {
					// ... instantiate the rocket facing right and set it's velocity to the right. 
					bulletRotation = Quaternion.Euler(Vector3.zero);
					bulletVelocity = new Vector2(speed, 0);
				} else { //otherwise, face and move rocket to left
					bulletRotation = Quaternion.Euler(Vector3.left); //I think this property is correct
					bulletVelocity = new Vector2(-speed, 0);
				}
				//spawn the bullet. cache it to apply velocity
				Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, bulletRotation) as Rigidbody2D;
				bulletInstance.velocity = bulletVelocity;
			}
		} else {
			//cursor position is in screen corrdinates. Need to translate this to world position.
			cursorPos = Input.mousePosition;
			cursorInWorld = Camera.main.ScreenToWorldPoint(cursorPos);
			//get the difference between my position and the cursor's
			float resultX = cursorInWorld.x - transform.position.x;
			float resultY = cursorInWorld.y - transform.position.y;
			//get the angle
			//I had to look this bit up. I could not find a method that did this math for me; Vector3.Angle returns between 0 and 180
			float angle = Mathf.Atan2(resultY, resultX) * Mathf.Rad2Deg;
			//the whole player (gun is a child thus flips too) transform flips when moving left, so this angle needs to flip to match the gun's change
			if (!playerMoveScript.FacingRight) {
				angle *= -1;
			}
			//manually set rotation so that the gun is rotated to point at cursor position
			transform.rotation = Quaternion.Euler(0, 0, angle);

			if(Input.GetButtonDown("Fire1")) {
				//bulletRotation = Quaternion.Euler(new Vector3(resultX, resultY));
				bulletRotation = transform.rotation; //maybe this works?
				//normalize vector before multiplying by speed so that movement speed is similar in both this and horizontal shooting
				bulletVelocity = new Vector2(resultX, resultY).normalized * speed;
				//spawn the bullet. cache it to apply velocity
				Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, bulletRotation) as Rigidbody2D;
				bulletInstance.velocity = bulletVelocity;
			}
		}
	}
}
