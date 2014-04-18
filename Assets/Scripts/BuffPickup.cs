using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))] //not sure if even helpful
public class BuffPickup : MonoBehaviour {
	/// <summary>
	/// Types of buffs player can pick up.
	/// </summary>
	public enum BuffTypes { 
		/// <summary>
		/// Heals the player
		/// </summary>
		HEALTH,
		/// <summary>
		/// Increase damage of the gun player is using
		/// </summary>
		DAMAGE_UP,
		/// <summary>
		/// Increase amount of grenades player is carrying
		/// </summary>
		AMMO_SECONDARY
	}
	/// <summary>
	/// Occurs when player picks up a buff
	/// </summary>
	public static event System.Action<BuffTypes, int> BuffPlayer;

	[SerializeField]
	private BuffTypes type;
	[SerializeField]
	private int amount; //amount the buff increases the stat by
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (BuffPlayer != null) {
				BuffPlayer(type, amount);
			}
			Destroy(gameObject); //remove buff item from scene
		}
	}
}