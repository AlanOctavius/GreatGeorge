using UnityEngine;
using System.Collections;

public class PlayerHealth : Character {

	/// <summary>
	/// Occurs when player dies from falling or health reaches 0.
	/// </summary>
	public static event System.Action playerDeath;

	void Start() {
		health = 10;
	}

	protected override void Die() {
		if (playerDeath != null) {
			playerDeath();
		}
	}

	/// <summary>
	/// Should call when player falls to his death.
	/// </summary>
	public void PlayerFell() {
		Die();
	}
}
