using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IShootable {

	void Start() {
		
	}

	void Update() {
		
	}

	public void TakeDamage (int damage)
	{
		print("Took "+ damage + " damage");
	}
}
