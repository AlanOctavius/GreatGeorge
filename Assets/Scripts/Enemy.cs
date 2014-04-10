using UnityEngine;
using System.Collections;

//DEBUG this class is a test enemy only.

public class Enemy : Character {

	void Start() {
		health = 10;
	}

	protected override void Die() {
		Destroy(gameObject);
	}
}
