using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour, IDamageable {

	/// <summary>
	/// The health of the Character. It needs to be set to a default value by an inheriting class, probably in its Start method. Public read access though <see cref="Health"/>property.
	/// </summary>
	protected int health;
	//Not sure if we need public read access to health. maybe if we have health bars over the enemies? But here is a read-only property if we do.
	/// <summary>
	/// The current health of the <see cref="Character"/>
	/// </summary>
	/// <value>The health.</value>
	public int Health {
		get { return health; } }

	/// <summary>
	/// If the <see cref="Character"/>doesn't take the raw amount of <paramref name="damage"/>, such as if it has armor, you need to override this method.
	/// </summary>
	/// <param name="damage">Damage to remove from health</param>
	public virtual void TakeDamage (int damage)	{
		StartCoroutine(FlashColor());
		if (damage > 0) {
			health -= damage;
		}
		if (health <= 0) {
			Die();
		}
		print(transform.tag + " took " + damage + " damage. Health: " + Health.ToString());
	}

	public IEnumerator FlashColor() {
		renderer.material.color = Color.red;
		yield return new WaitForSeconds(0.2f);
		renderer.material.color = Color.white;
	}

	/*I figured everyone can implement their own death. but I guess it may be better for this to be virtual. we can change it if need be.
	 Player death will need to call game end. Boss death may have something special. enemy death just needs to increment points (or do nothing?).
	 If different enemies give different points, having this stay abstract is probably best
	 */
	/// <summary>
	/// Called when Character's <see cref="Health"/>reaches 0. Enemies should increment score w/ <see cref="GameManagerScript"/>and call Destroy on self
	/// </summary>
	/// <example>
	/// <c>
	/// Destroy(gameObject);
	/// </c>
	/// </example>
	protected abstract void Die();
}