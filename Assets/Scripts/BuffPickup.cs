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
		DAMAGE_UP }
	/// <summary>
	/// Occurs when player picks up a buff
	/// </summary>
	public static event System.Action<BuffTypes, int> buffPlayer;

	[SerializeField]
	private BuffTypes type;
	[SerializeField]
	private int amount; //amount the buff increases the stat by
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (buffPlayer != null) {
				buffPlayer(type, amount);
			}
			Destroy(gameObject); //remove buff from scene
		}
	}
}