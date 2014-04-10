using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	//TODO once we decide whether we are shooting horizontally or at cursor, remove boolean (and check and other heloer method). otherwise, each gun picked up can change this behavior

	private PlayerMovement playerMoveScript; //init in Start

	[SerializeField] private Rigidbody2D bullet;  //assigned in inspector
	[SerializeField] private int bulletDamage = 1; //different guns can cause differnt damage
	private BulletScript bs; //member so not creating a new var every shot
	private Rigidbody2D bulletInstance; //member so not creating a new var every shot
	private Quaternion bulletRotation; //current bullet is round so its rotation doesn't matter. but if that changes, we do have to change its orientation as it's done below
	private Vector2 bulletVelocity; //determined at bullet instantiation
	//DEBUG serialized for testing
	[SerializeField] private float bulletSpeed = 20f;   // The speed the bullet will fire at.

	//DEBUG serialized for testing
	[SerializeField] private bool shootAtCursor = false;
	private Vector3 cursorPos;
	private Vector3 cursorInWorld;

	void Awake() {
		//in Awake for 2 reasons: 1, ensures this is set false before possible re-enable in Start of PickUpGunScript (all Awakes happen before any Starts)
		//and 2, Awake happens before OnEnable while Start doesn't. this is probably less important though.
		this.enabled = false;
	}

	void Start() {
		playerMoveScript = transform.root.GetComponent<PlayerMovement>();
	}

	void OnEnable() {
		BuffPickup.buffPlayer += CheckDamageBuff;
	}
	
	void OnDisable() {
		BuffPickup.buffPlayer -= CheckDamageBuff;
	}

	void Update() {
		if (Input.GetButtonDown("Fire1") && !GameManagerScript.Paused) { //better way of checking pause?
			//set velocity (and rotation) in helper methods
			if (!shootAtCursor) { //shoot only left and right
				ShootHorizontal();
			} else { ShootAtCursor(); }
			//spawn the bullet. cache it to set velocity and damage
			bulletInstance = Instantiate(bullet, transform.position, bulletRotation) as Rigidbody2D;
			bulletInstance.velocity = bulletVelocity; //set bullet velocity on rigidbody
			//set bullet damage after getting script attached to same GO as rigidbody. is there a better way so don't need to getComponent here?
			bs = bulletInstance.GetComponent<BulletScript>() as BulletScript;
			bs.Damage = bulletDamage;
		}
	}

	//helper sets velocity and rotation of bullet
	void ShootHorizontal() {
		// If the player is facing right...
		if(playerMoveScript.FacingRight) {
			// ... instantiate the bullet facing right and set it's velocity to the right. 
			bulletRotation = Quaternion.Euler(Vector3.zero);
			bulletVelocity = new Vector2(bulletSpeed, 0);
		} else { //otherwise, face and move rocket to left
			bulletRotation = Quaternion.Euler(Vector3.left); //I think this property is correct direction
			bulletVelocity = new Vector2(-bulletSpeed, 0);
		}
	}

	//helper sets velocity and rotation of bullet (and rotation of gun)
	void ShootAtCursor() {
		//cursor position is in screen corrdinates. Need to translate this to world position.
		cursorPos = Input.mousePosition;
		cursorInWorld = Camera.main.ScreenToWorldPoint(cursorPos);
		//get the difference between my position and the cursor's
		float resultX = cursorInWorld.x - transform.position.x;
		float resultY = cursorInWorld.y - transform.position.y;
		//calc the angle
		//I had to look this bit up. I could not find a method that did this math for me; Vector3.Angle returns between 0 and 180
		float angle = Mathf.Atan2(resultY, resultX) * Mathf.Rad2Deg;
		//the whole player transform (gun is a child and thus it too) flips when moving left, so this angle needs to flip to match the gun's change
		if (!playerMoveScript.FacingRight) {
			angle *= -1;
		}
		//manually set rotation so that the gun is rotated to point at cursor position
		transform.rotation = Quaternion.Euler(0, 0, angle);
		bulletRotation = transform.rotation; //maybe this works?
		//normalize vector before multiplying by speed so that movement speed is similar in both this and horizontal shooting
		bulletVelocity = new Vector2(resultX, resultY).normalized * bulletSpeed;
	}

	void CheckDamageBuff(BuffPickup.BuffTypes type, int amount) {
		if (type == BuffPickup.BuffTypes.DAMAGE_UP && this.enabled)
			bulletDamage += amount;
	}
}
