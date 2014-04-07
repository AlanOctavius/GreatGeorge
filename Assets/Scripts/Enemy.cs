using UnityEngine;
using System.Collections;

public class Enemy : Character {

	void Start() {
		health = 10;
	}

	protected override void Die() {
		Destroy(gameObject);
	}
}
