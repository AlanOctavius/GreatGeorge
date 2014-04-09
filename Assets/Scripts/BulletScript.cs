using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	private int damage = 1;
	/// <summary>
	/// Gets or sets the <paramref name="damage"/>the bullet deals
	/// </summary>
	/// <value>The damage the bullet deals</value>
	public int Damage {
		get {
			return this.damage;
		}
		set {
			if (value >= 0) damage = value;
		}
	}

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Enemy") {
			//e.g.:
			//EnemyScript es = col.gameObject.GetComponent<EnemyScript>()
			//es.TakeDamage(damage);
			//Destroy(gameObject); //destroy bullet
		}
		//I don't know if this is the best way. Using non-generic GetComponent can be costly, I believe. And seems a bit roundabout when we can just check tag and use a known script name.
		else {
			IDamageable script = col.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
			if (script != null && col.tag != "Player") { //TODO if we make enemies shoot guns, explicit player tag check will have to be changed
				script.TakeDamage(damage);
				Destroy(gameObject); //destroy bullet
			}
		}
	}
}
