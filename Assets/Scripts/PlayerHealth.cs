using UnityEngine;
using System.Collections;

public class PlayerHealth : Character {

	public static event System.Action playerDeath;

	void Start() {
		health = 10;
	}

	void OnEnable() {

	}

	void OnDisable() {

	}

	protected override void Die() {
		if (playerDeath != null) {
			playerDeath();
		}
	}

	public void PlayerFell() {
		Die();
	}
}
