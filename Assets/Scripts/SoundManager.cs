using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour {

	[SerializeField] private AudioClip healthSound;
	[SerializeField] private AudioClip ammoSound;

	void OnEnable() {
		BuffPickup.BuffPlayer += PlayPickupSound;
	}
	
	void OnDisable() {
		BuffPickup.BuffPlayer -= PlayPickupSound;
	}

	void PlayPickupSound(BuffPickup.BuffTypes pickupType, int amount) {
		if (pickupType == BuffPickup.BuffTypes.HEALTH) {
			audio.clip = healthSound;
		} else if (pickupType == BuffPickup.BuffTypes.AMMO_SECONDARY) {
			audio.clip = ammoSound;
		}

		if (audio != null && audio.clip != null) {
			audio.Play();
		}
	}
}
