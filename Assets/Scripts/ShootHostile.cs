using UnityEngine;
using System.Collections;

public class ShootHostile : TouchEnemy {

	// Use this for initialization

	public float shootTime = 5f;		// The amount of time between each spawn.
	public float shootDelay = 3f;		// The amount of time before spawning starts.
	public float bulletGravityScale = 0.5f; // Gravity scale for bullets, to give the hostile some range
	public Rigidbody2D bullet;  //assigned in inspector

	private Rigidbody2D bulletInstance; //member so not creating a new var every shot
	private Quaternion bulletRotation; //current bullet is round so its rotation doesn't matter. but if that changes, we do have to change its orientation as it's done below
	private Vector2 bulletVelocity; //determined at bullet instantiation
	private BulletScript bs;
	private int bulletDamage = 1; //different guns can cause differnt damage

	private float bulletSpeed = 20f;   // The speed the bullet will fire at.

	private Animator animator;	

	protected override void ExtraStart () {
		//aheadGroundCheck.localPosition =  Vector3.Scale( invertYVec , aheadGroundCheck.localPosition );
		aheadContactCheck.localPosition =  Vector3.Scale( invertYVec , aheadContactCheck.localPosition );
		InvokeRepeating("Shoot", shootDelay, shootTime);
		pointValue = 15;

		animator = GetComponent<Animator>() as Animator;
	}


	protected void Shoot (){

		bulletRotation = Quaternion.Euler(Vector3.zero);
		bulletVelocity = new Vector2(bulletSpeed*movingDirection, 0);
		//set velocity (and rotation) in helper methods
		//spawn the bullet. cache it to set velocity and damage
		bulletInstance = Instantiate(bullet, transform.position, bulletRotation) as Rigidbody2D;
		bulletInstance.velocity = bulletVelocity; //set bullet velocity on rigidbody
		//set bullet damage after getting script attached to same GO as rigidbody. is there a better way so don't need to getComponent here?
		bs = bulletInstance.GetComponent<BulletScript>() as BulletScript;
		bs.Damage = bulletDamage;
		bs.ShooterTag = tag; //when gun is child of character, need to use parent's tag
		//bs.rigidbody2D.gravityScale = bulletGravityScale;
		animator.SetTrigger("Attacking");

		audio.Play();
	}

}
