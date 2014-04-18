using UnityEngine;
using System.Collections;

public class PlayerHealth : Character {

	/// <summary>
	/// Occurs when player dies from falling off level or health reaches 0.
	/// </summary>
	public static event System.Action playerDeath;

	void Start() {
		health = 10; //health (in Character)
	}

	void OnEnable() {
		BuffPickup.BuffPlayer += CheckHealthBuff;
	}

	void OnDisable() {
		BuffPickup.BuffPlayer -= CheckHealthBuff;
	}

	protected override void Die() {
		if (playerDeath != null) {
			playerDeath();
		}
	}

	/// <summary>
	/// Should call when player falls out of level to his death.
	/// </summary>
	public void PlayerFell() {
		Die();
	}

	private void CheckHealthBuff(BuffPickup.BuffTypes type, int amount) {
		if (type == BuffPickup.BuffTypes.HEALTH) {
			print("Player health: " + Health); //DEBUG print health before and after pickup buff
			health += amount;
			print("Player health: " + Health);
		}
	}
}
