using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour, IDamageable {

	protected int health;
	//Not sure if we need public-read access to health. maybe if we have health bars over the enemies? But here is a read-only property if we do.
	public int Health {
		get { return health; } }

	void Start() {
		health = 10;
	}

	//maybe this will need to be overridden if something has armor or a shield or something, so it's virtual
	public virtual void TakeDamage (int damage)	{
		if (damage > 0) {
			health -= damage;
			print(health);
		}
		if (health <= 0) {
			Die();
		}
	}

	//I figured everyone can implement their own death. but I guess it may be better for this to be virtual. we can change it if need be.
	//Player death will need to call game end. Boss death may have something special. enemy death just needs to increment points (or do nothing?).
	//If different enemies give different points, having this stay abstract is probably best
	protected abstract void Die();
}