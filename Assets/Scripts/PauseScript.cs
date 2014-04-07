using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	public enum PauseReason {Pause, PlayerDie, EnemyWin };
	[Range(25,100)]
	public float menuSizePercent = 80f;

	public PauseReason Reason { get; set; }

	private float scrnW;
	private float scrnH;

	private float offsetFromTop;
	private float offsetFromLeft;
	private float height;
	private float width;

	public float buttonWidth = 200f;
	public float buttonHeight = 75f;
	public float buttonSpacing = 20f;

	void OnEnable() {
		Time.timeScale = 0.0f;
	}

	void OnDisable() {
		Time.timeScale = 1.0f;
	}

	void Start() {
		scrnW = Screen.width;
		scrnH = Screen.height;

		offsetFromLeft = (scrnW * ((100f - menuSizePercent) / 100f) / 2);
		offsetFromTop = (scrnH * ((100f - menuSizePercent) / 100f) / 2);
		height = scrnH * (menuSizePercent / 100f);
		width = scrnW * (menuSizePercent / 100f);
	}

	void OnGUI() {
		offsetFromLeft = (scrnW * ((100f - menuSizePercent) / 100f) / 2);
		offsetFromTop = (scrnH * ((100f - menuSizePercent) / 100f) / 2);
		height = scrnH * (menuSizePercent / 100f);
		width = scrnW * (menuSizePercent / 100f);

		GUI.BeginGroup(new Rect(offsetFromLeft, offsetFromTop, width, height));
		GUI.Box(new Rect(0,0, width, height), "");

		GUI.Label(new Rect(width / 2 - 20, 50, 100, 25), Reason.ToString());

		if (Reason == PauseReason.Pause) {
			if (GUI.Button(new Rect(width / 2 - buttonWidth / 2, buttonHeight * 2, buttonWidth, buttonHeight), "Resume")) {
				GameManagerScript.UnpauseGame();
			}
		}

		if (GUI.Button(new Rect(width / 2 - buttonWidth / 2, buttonHeight * 3 + buttonSpacing, buttonWidth, buttonHeight), "Restart")) {
			GameManagerScript.RestartScene();
		}

		if (GUI.Button(new Rect(width / 2 - buttonWidth / 2, buttonHeight * 4 + (buttonSpacing * 2), buttonWidth, buttonHeight), "Exit Game")) {
			GameManagerScript.EndGame();
		}

		GUI.EndGroup();
	}
}
