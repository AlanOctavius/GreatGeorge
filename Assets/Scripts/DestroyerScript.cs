using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Player") {
			PlayerHealth ph = coll.gameObject.GetComponent<PlayerHealth>() as PlayerHealth;
			ph.PlayerFell();
		} else if (coll.tag == "Hostile") {
			Destroy(coll.gameObject);
		} else {
			//destroys anything that collides with this, but hopefully nothing will other than enemies.
			//Destroy(coll.gameObject);
		}
	}
}
