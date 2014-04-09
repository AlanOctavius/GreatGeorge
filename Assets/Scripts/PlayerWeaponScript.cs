using UnityEngine;
using System.Collections;

public class PlayerWeaponScript : MonoBehaviour {

	//An empty GO exists on player for use as a position to set the gun to. this is a reference to it's transform.
	//Could use a hard-coded position, but this is prettier. If guns need to be placed at different positions (a rocket launcher wouldn't go to same place as a handgun), need to use something else.
	private Transform gunPosition;
	private GunScript gs; //holds the currently-equipped gun's script for access to it

	void Start() {
		gunPosition = transform.Find("GunPosition");
		gs = GetComponentInChildren<GunScript>() as GunScript; //If player doesn't start with gun, will be null.
		if (gs != null) {
			gs.enabled = true; //if player does start with a gun, its script should be enabled at start of level.
		}
	}

	/// <summary>
	/// When a gun gets touched, call this method for player to equip it.
	/// </summary>
	/// <param name="gun">Picked up gun's transform</param>
	public void PickUpGun(Transform gun) {
		if (gs == null) { //if no recollection of currently equipped gun (e.g. player didn't have one at level start)
			gs = GetComponentInChildren<GunScript>() as GunScript; //try again to find a gun on player. may never happen since gs gets set after every pickup. but should check anyway?
		}
		if (gs != null) { //if player does have a gun equipped (gs will only be not null after picking up a gun)
			Destroy(gs.gameObject); //throw it away; destroy the GO
		}
		gun.parent = transform; //child the gun so it moves with player
		if (gunPosition != null) gun.position = gunPosition.position; //set its position to that of pre-determined gun position for player
		gs = gun.GetComponent<GunScript>() as GunScript; //set new gun's script as gs
		if (gs != null) {
			gs.enabled = true; //if didn't fail in attempt to do that, enable the script so gun can be fired
		}
	}
}
