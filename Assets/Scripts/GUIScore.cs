using UnityEngine;
using System.Collections;

public class GUIScore : MonoBehaviour {

	private GUIStyle blackText;

	void Start() {
		blackText = new GUIStyle();
		blackText.normal.textColor = Color.black;
	}

	void OnGUI(){
		GUI.Label (new Rect (20,30,100,50), "Score: " + GameManagerScript.GameScore.ToString (), blackText);
	}
	
}