using UnityEngine;
using System.Collections;

public class GUIHealth : MonoBehaviour {
		
	private PlayerHealth player;
	private GUIStyle blackText;
	//private GUIStyle oldStyle;

	void Start(){
		player = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
		blackText = new GUIStyle();
		blackText.normal.textColor = Color.black;
		}

	void OnGUI(){
		GUI.Label (new Rect (20,10,100,50), "Health: " + player.Health, blackText);
		}
		
	}