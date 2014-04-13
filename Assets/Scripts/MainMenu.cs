using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	//DEBUG serialized for testing
	[SerializeField, Range(25,100)]
	private float menuSizePercent = 80f;

	private float scrnW;
	private float scrnH;

	//These variables are calculated in Start and depend upon the screen size and menuSizePercent to set the pause menu screen as a percentage of the whole screen
	private float offsetFromTop;
	private float offsetFromLeft;
	private float height;
	private float width;

	//DEBGUG serialized for testing
	[SerializeField] private float buttonWidth = 175f;
	[SerializeField] private float buttonHeight = 50f;
	[SerializeField] private float buttonSpacing = 20f;

	private GUIStyle centeredLabel;

	void Start() {
		scrnW = Screen.width;
		scrnH = Screen.height;

		//offset is the difference from the screen percentage (90% size screen is 10% here) divided in half
		offsetFromLeft = (scrnW * ((100f - menuSizePercent) / 100f)) / 2;
		offsetFromTop = (scrnH * ((100f - menuSizePercent) / 100f)) / 2;
		//menu dimension is the specified percentage of the whole screen. e.g. 90%
		height = scrnH * (menuSizePercent / 100f);
		width = scrnW * (menuSizePercent / 100f) * 0.25f;
	}

	void OnGUI() {
		centeredLabel = new GUIStyle(GUI.skin.label);
		centeredLabel.alignment = TextAnchor.UpperCenter;

		GUI.BeginGroup(new Rect(offsetFromLeft, offsetFromTop, width, height));
		GUI.Box(new Rect(0,0, width, height), "");

		GUI.Label(new Rect(width / 2 - 50, 50, 100, 25), "Great George", centeredLabel);		

		if (GUI.Button(new Rect(width / 2 - buttonWidth / 2, buttonHeight * 2, buttonWidth, buttonHeight), "Play")) {
			Application.LoadLevel(1);
		}

		if (GUI.Button(new Rect(width / 2 - buttonWidth / 2, buttonHeight * 3 + buttonSpacing, buttonWidth, buttonHeight), "Exit Game")) {
			Application.Quit();
		}

		GUI.Label(new Rect(width / 2 - 60, 390, 120, 25), "Art", centeredLabel);
		GUI.Label(new Rect(width / 2 - 60, 410, 120, 25), "Alex Sherman", centeredLabel);
		GUI.Label(new Rect(width / 2 - 60, 450, 120, 25), "Programming", centeredLabel);
		GUI.Label(new Rect(width / 2 - 60, 470, 120, 25), "Alan Octavius", centeredLabel);
		GUI.Label(new Rect(width / 2 - 60, 490, 120, 25), "David Cottingham", centeredLabel);

		GUI.EndGroup();
	}
}
