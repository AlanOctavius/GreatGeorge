using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D)), RequireComponent (typeof(GunScript))] //not sure if even helpful
public class PickUpGunScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			//could handle here or on player. move position may be better to handle on player for easier access to GunPosition
			/*transform.parent = other.transform;
			Transform gunAttach = other.transform.Find("GunPosition");
			if (gunAttach != null) {
				transform.position = gunAttach.position;
			}
			GunScript gs = GetComponent<GunScript>() as GunScript;
			gs.enabled = true;*/
			PlayerWeaponScript pw = other.GetComponent<PlayerWeaponScript>() as PlayerWeaponScript;
			pw.PickUpGun(transform); //tell player to pick us up
			//this.enabled = false; //Does nothing since enabled only changes Update method. if triggering again occurs, need to implement own boolean and check when onTrigger occurs whether or ignore
		}
	}
}
