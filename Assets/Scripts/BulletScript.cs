using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CircleCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class BulletScript : MonoBehaviour {

	public string ShooterTag { get; set; }
	//for normal bullets, this value may be overwritten by changes to the Damage property by gun
	[SerializeField] private int damage = 1;
	[SerializeField] private bool isGrenade = false;
	[SerializeField] private float damageRadius = 0;
	[SerializeField] private float fuseTime = 0;

	/// <summary>
	/// Gets or sets the <paramref name="damage"/>the bullet deals. Only sets if <paramref name="damage"/> is not negative. Currently allows 0 damage in case we use pickups that debuff player to doing no damage.
	/// </summary>
	/// <value>The damage the bullet deals</value>
	public int Damage {
		get {
			return this.damage;
		} set {
			if (value >= 0) damage = value;
		}
	}

	void Start () {
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		if (damageRadius <=0) isGrenade = false;
		if (isGrenade) {
			Invoke("Explode", fuseTime);
			gameObject.layer = 10;
		} else {
			Destroy(gameObject, 2);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (!isGrenade) {
			IDamageable script = col.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
			if (script != null && col.tag != ShooterTag) {
				script.TakeDamage(damage);
				//Destroy(gameObject); //destroy bullet
			}
			//Destroy when hit ground or enemies
			if (col.tag == "ground" || (ShooterTag == "Hostile" ? col.tag == "Player" : false) || (ShooterTag == "Player" ? col.tag == "Hostile" : false)) Destroy(gameObject); //destroy bullet
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (isGrenade) {
			rigidbody2D.drag = 0.25f;
		}
	}

	void Explode() {
		Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, damageRadius);
		foreach (Collider2D col in colls) {
			IDamageable script = col.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
			if (script != null && col.tag != ShooterTag) {
				script.TakeDamage(damage);
			}
		}
		Destroy(gameObject); //destroy grenade
	}
}
