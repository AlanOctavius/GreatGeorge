using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	private static int score = 0;
	public static int GameScore { 
		get { return score; } }

	private static PauseScript pauseScript;
	private static bool paused = false;
	/// <summary>
	/// Whether the game is paused or not
	/// </summary>
	/// <value><c>true</c> if game is currently paused; otherwise, <c>false</c>.</value>
	public static bool Paused { 
		get { return paused; } }
	private static bool gameOver = false;

	void Start() {
		pauseScript = gameObject.GetComponent<PauseScript>() as PauseScript;
	}

	void Update() {
		//pause key check
		if (Input.GetButtonDown("Pause")) {
			if (!paused)
				PauseGame();
			else if (!gameOver)
				UnpauseGame();
		}
	}

	//register the playerDeath event
	void OnEnable() {
		PlayerHealth.playerDeath += PlayerDied;
	}

	//Need to also un-register every event you registered
	void OnDisable() {
		PlayerHealth.playerDeath -= PlayerDied;
	}

	void PlayerDied() {
		//if player has more than one life, handle that here
		gameOver = true;
		PauseGame(PauseScript.PauseReason.PlayerDie);
	}

	/// <summary>
	/// Restart the current scene. Only <see cref="PauseScript"/>should call, really.
	/// </summary>
	public static void RestartScene() {
		Application.LoadLevel(Application.loadedLevel);
	}

	/// <summary>
	/// Close the game. Only <see cref="PauseScript"/>should call, really.
	/// </summary>
	public static void EndGame() {
		Application.Quit();
	}

	/// <summary>
	/// Pause the game for the specified <see cref="PauseReason"/>
	/// </summary>
	/// <param name="reason"><paramref name="reason"/> for pausing the game.</param>
	private static void PauseGame(PauseScript.PauseReason reason) {
		paused = true;
		pauseScript.Reason = reason;
		pauseScript.enabled = true;
	}

	/// <summary>
	/// Player-initiated pausing of the game
	/// </summary>
	public static void PauseGame() {
		PauseGame(PauseScript.PauseReason.Pause);
	}

	/// <summary>
	/// Unpause the game
	/// </summary>
	public static void UnpauseGame() {
		pauseScript.enabled = false;
		paused = false;
	}

	/// <summary>
	/// Increases the player's score.
	/// </summary>
	/// <returns>The player's total score</returns>
	/// <param name="score">How much score to give the player</param>
	public static void IncreaseScore(int scoreInc) {
		score += scoreInc;

	}
}
