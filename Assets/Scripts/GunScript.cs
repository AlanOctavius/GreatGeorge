using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	//TODO once we decide whether we are shooting horizontally or at cursor, remove boolean (and check and other heloer method). otherwise, each gun picked up can change this behavior

	private PlayerMovement playerMoveScript; //init in Start

	[SerializeField] private Rigidbody2D bullet;  //assigned in inspector
	[SerializeField] private Rigidbody2D grenade;  //assigned in inspector

	[SerializeField] private int bulletDamage; // used in inspector.
	public int BulletDamage { 
		get { return bulletDamage; }
		set { bulletDamage = value; } } //different guns or player buff pickup can cause different damage
	private BulletScript bs; //member so not creating a new var every shot
	private Rigidbody2D projectileInstance; //member so not creating a new var every shot
	private Quaternion bulletRotation; //current bullet is round so its rotation doesn't matter. but if that changes, we do have to change its orientation as it's done below
	private Vector2 projectileVelocity; //determined at bullet instantiation
	//DEBUG serialized for testing
	[SerializeField] private float bulletSpeed = 20f;   // The speed the bullet will fire at.
	[SerializeField] private float grenadeSpeed = 20f;   // The speed the bullet will fire at.

	private Vector3 cursorPos;
	private Vector3 cursorInWorld;

	//So that we aren't creating and destroying these vars every frame.
	private float resultX;
	private float resultY;
	private float angle;

	//these need to be set before each shot at least. every frame if want gun to rotate real time
	public Vector3 TargetLocation { get; set; }
	public bool FacingRight { get; set; }

	void Awake() {
		//in Awake for 2 reasons: 1, ensures this is set false before possible re-enable in Start of PickUpGunScript (all Awakes happen before any Starts)
		//and 2, Awake happens before OnEnable while Start doesn't. this is probably less important though.
		this.enabled = false;
	}

	public void Shoot() {
		bulletRotation = transform.rotation; //maybe this works?
		//normalize vector before multiplying by speed so that movement speed is similar in both this and horizontal shooting
		projectileVelocity = new Vector2(resultX, resultY).normalized * bulletSpeed;
		//spawn the bullet. cache it to set velocity and damage
		projectileInstance = Instantiate(bullet, transform.position, bulletRotation) as Rigidbody2D;
		projectileInstance.velocity = projectileVelocity; //set bullet velocity on rigidbody
		//set bullet damage after getting script attached to same GO as rigidbody. is there a better way so don't need to getComponent here?
		bs = projectileInstance.GetComponent<BulletScript>() as BulletScript;
		bs.Damage = BulletDamage;
		bs.ShooterTag = transform.parent.tag; //when gun is child of character, need to use parent's tag
	}

	public void ShootSecondary() {
		projectileVelocity = new Vector2(resultX, resultY).normalized * grenadeSpeed;
		//spawn the bullet. cache it to set velocity and damage
		projectileInstance = Instantiate(grenade, transform.position, Quaternion.identity) as Rigidbody2D;
		projectileInstance.velocity = projectileVelocity; //set bullet velocity on rigidbody
		//set bullet damage after getting script attached to same GO as rigidbody. is there a better way so don't need to getComponent here?
		bs = projectileInstance.GetComponent<BulletScript>() as BulletScript;
		//damage onnly default, set in grenade prefab
		bs.ShooterTag = transform.parent.tag; //when gun is child of character, need to use parent's tag
	}

	//DEBUG temp display for testing
	void OnGUI() {
		GUI.Label(new Rect(0, 60, 200, 20), resultX.ToString("F1"));
		GUI.Label(new Rect(0, 75, 200, 20), resultY.ToString("F1"));
		GUI.Label(new Rect(0, 90, 200, 20), angle.ToString("F1"));
	}

	//TODO fix player facing so works with enemy facing direction
	//helper sets velocity and rotation of bullet (and rotation of gun)
	void Update() {
		//get the difference between my position and the cursor's
		resultX = TargetLocation.x - transform.position.x;
		resultY = TargetLocation.y - transform.position.y;
		//calc the angle
		//I had to look this bit up. I could not find a method that did this math for me; Vector3.Angle returns between 0 and 180
		angle = Mathf.Atan2(resultY, resultX) * Mathf.Rad2Deg;
		//the whole player transform (gun is a child and thus it too) flips when moving left, so this angle needs to flip to match the gun's change
		if (!FacingRight) {
			angle *= -1;
			//gun angle:
			//manual clamping needed since angle goes 90 - 180 or -90 - -180
			if (angle >= 0 && angle <= 90) {
				angle = 90f;
			} else if (angle >= -90f && angle <= 0) {
				angle = -90f;
			}
			//bullet X velocity clamp:
			if (resultX > 0) resultX = 0;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		} else {
			//manually set rotation so that the gun is rotated to point at cursor position
			transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(angle, -90, 90f));
			//bullet X velocity clamp:
			if (resultX < 0) resultX = 0;
		}
	}
}
