using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	void OnEnable() {
		PlayerHealth.playerDeath += PlayerDied;
	}

	void OnDisable() {
		PlayerHealth.playerDeath -= PlayerDied;
	}

	void PlayerDied() {
		//if player has more than one life, handle that here
		//TEMP restart immediately after death
		RestartScene();
	}

	void RestartScene() {
		Application.LoadLevel(Application.loadedLevel);
	}
}
