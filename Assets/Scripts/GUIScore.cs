using UnityEngine;
using System.Collections;

public class GUIScore : MonoBehaviour {


	void OnGUI(){
		GUI.Label (new Rect (0,20,100,50), "ScoreGUI : " + GameManagerScript.GameScore.ToString ());
	}
	
}