using UnityEngine;
using System.Collections;

public class GUIHealth : MonoBehaviour {
		
	private PlayerHealth player;

	void Start(){
		player = GameObject.Find ("Player").GetComponent<PlayerHealth> ();

		}

	void OnGUI(){
		GUI.Label (new Rect (0,0,100,50), "Health: " + player.Health);
		}
		
	}