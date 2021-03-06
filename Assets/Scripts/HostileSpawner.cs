﻿using UnityEngine;
using System.Collections;

public class HostileSpawner : MonoBehaviour {

	public GameObject hostile;
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public int spawnDirection = 1;

	// Use this for initialization
	void Start () {
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);

	}
	
	void Spawn ()
	{
		// Instantiate a hostile.

		GameObject enemy = Instantiate(hostile, transform.position, transform.rotation) as GameObject;

		TouchEnemy script = enemy.GetComponent<TouchEnemy>() as TouchEnemy;
		script.movingDirection = spawnDirection;	
	}
}
