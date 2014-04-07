using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	private static PauseScript pauseScript;
	private static bool paused = false;
	public static bool Paused { 
		get { return paused; } }

	void Start() {
		pauseScript = gameObject.GetComponent<PauseScript>() as PauseScript;
	}

	void Update() {
		//pause key check
		if (Input.GetButtonDown("Pause")) {
			if (!paused)
				PauseGame();
			else
				UnpauseGame();
		}
	}

	void OnEnable() {
		PlayerHealth.playerDeath += PlayerDied;
	}

	void OnDisable() {
		PlayerHealth.playerDeath -= PlayerDied;
	}

	void PlayerDied() {
		//if player has more than one life, handle that here
		PauseGame(PauseScript.PauseReason.PlayerDie);
	}

	public static void RestartScene() {
		Application.LoadLevel(Application.loadedLevel);
	}

	public static void EndGame() {
		Application.Quit();
	}

	private static void PauseGame(PauseScript.PauseReason reason) {
		paused = true;
		pauseScript.Reason = reason;
		pauseScript.enabled = true;
	}

	public static void PauseGame() {
		PauseGame(PauseScript.PauseReason.Pause);
	}

	public static void UnpauseGame() {
		pauseScript.enabled = false;
		paused = false;
	}
}
