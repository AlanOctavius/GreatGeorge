/// <summary>
/// Implementer can take damage (from a weapon)
/// </summary>
public interface IDamageable {
	/// <summary>
	/// Take specified amount of <paramref name="damage"/>(from a weapon)
	/// </summary>
	/// <param name="damage">Damage amount taken</param>
	void TakeDamage(int damage);
}