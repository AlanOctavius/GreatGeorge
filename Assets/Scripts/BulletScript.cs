using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	void Start () 
	{
		// Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 2);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Enemy") {
			//e.g.:
			//EnemyScript es = col.gameObject.GetComponent<EnemyScript>()
			//es.TakeDamage(1);
		}
		//I'd rather not do it this way. Using non-generic GetComponent can be costly, I believe. And seems a bit roundabout when can just check tag and use a known script name.
		else {
			IShootable script = col.gameObject.GetComponent(typeof(IShootable)) as IShootable;
			if (script != null) {
				script.TakeDamage(1);
			}
		}
	}
}
