using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour, IDamageable {

	/// <summary>
	/// The health to be set to a default value by an inheriting class
	/// </summary>
	protected int health;
	//Not sure if we need public-read access to health. maybe if we have health bars over the enemies? But here is a read-only property if we do.
	/// <summary>
	/// Gets the health.
	/// </summary>
	/// <value>The health.</value>
	public int Health {
		get { return health; } }

	//maybe this will need to be overridden if something has armor or a shield or something, so it's virtual
	/// <summary>
	/// If the Character doesn't take the raw amount of damage, such as if it has armor, you need to override this method.
	/// </summary>
	/// <param name="damage">Damage to remove from health</param>
	public virtual void TakeDamage (int damage)	{
		if (damage > 0) {
			health -= damage;
			print(health);
		}
		if (health <= 0) {
			Die();
		}
	}

	/*I figured everyone can implement their own death. but I guess it may be better for this to be virtual. we can change it if need be.
	 Player death will need to call game end. Boss death may have something special. enemy death just needs to increment points (or do nothing?).
	 If different enemies give different points, having this stay abstract is probably best
	 */
	/// <summary>
	/// Called when Character's Health reaches 0. Should increment score w/ Game Manager and call Destroy on self
	/// </summary>
	/// <example>
	/// <c>
	/// Destroy(gameObject);
	/// </c>
	/// </example>
	protected abstract void Die();
}