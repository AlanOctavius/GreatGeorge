using UnityEngine;
using System.Collections;

public class ScoreDisplayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		updateScore ();
	}

	public void updateScore(){
			guiText.text = "Score : " + GameManagerScript.GameScore.ToString ();
		}

}
