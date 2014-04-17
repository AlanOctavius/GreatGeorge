using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour {

	//An empty GO exists on player for use as a position to set the gun to. this is a reference to it's transform.
	//Could use a hard-coded position, but this is prettier. If guns need to be placed at different positions (a rocket launcher wouldn't go to same place as a handgun), need to use something else.
	private Transform gunPosition;
	private BoxCollider2D gunCollider;
	private GunScript gs;
	private PlayerMovement ps;

	private int secondaryAmmo = 1;

	private Vector3 cursorPos;
	//private Vector3 cursorInWorld;

	void Start() {
		//get child gunscript
		gs = GetComponentInChildren<GunScript>() as GunScript;
		ps = GetComponent<PlayerMovement>() as PlayerMovement;
		gunPosition = transform.Find("GunPosition");

		if (gs != null) {
			gs.enabled = true; //if player does start with a gun, its script should be enabled at start of level.
		}
	}

	void Update() {
		cursorPos = Input.mousePosition;
		gs.TargetLocation = Camera.main.ScreenToWorldPoint(cursorPos);
		gs.FacingRight = ps.FacingRight;
		if (Input.GetButtonDown("Fire1") && !GameManagerScript.Paused) gs.Shoot();
		if (Input.GetButtonDown("Fire2") && !GameManagerScript.Paused) {
			if (secondaryAmmo > 0) {
				gs.ShootSecondary();
				--secondaryAmmo;
			}
		}
	}

	void OnEnable() {
		BuffPickup.BuffPlayer += CheckCombatBuff;
		BuffPickup.BuffPlayer += CheckCombatBuff;
	}
	
	void OnDisable() {
		BuffPickup.BuffPlayer -= CheckCombatBuff;
		BuffPickup.BuffPlayer -= CheckCombatBuff;
	}

	//bullet damage permanent boost? if actually use, may want to change implementation so has timer or something
	void CheckCombatBuff(BuffPickup.BuffTypes type, int amount) {
		if (type == BuffPickup.BuffTypes.DAMAGE_UP)
			gs.BulletDamage += amount;
		else if (type == BuffPickup.BuffTypes.AMMO_SECONDARY)
			secondaryAmmo =+ amount;
	}

	/// <summary>
	/// When a gun gets touched, call this method for player to equip it.
	/// </summary>
	/// <param name="gun">Picked up gun's transform</param>
	public void PickUpGun(Transform gun) {
		gs = GetComponentInChildren<GunScript>() as GunScript;
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
		
		gunCollider = gun.GetComponent<BoxCollider2D>() as BoxCollider2D;
		gunCollider.enabled = false;
	}
}
