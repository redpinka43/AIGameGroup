using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance = null;

	public GameObject playerObject;

	// We need to keep track of the current player. When we load the game, it tells us which player
	// we were playing as. Then it instantiates.... hmm.

	void Awake () {
		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "Player Man";
		}

		// Assign events
		MySceneManager.OnSceneChange += linkUp;
		MySceneManager.OnSceneChange += setupForNewScene;
	}

	// Use this for initialization
	void Start () {
		
	}

	void linkUp() {
		// Basically, if Gamestate = OVERWORLD, then we search the hierarchy for a "Player"
		
		if (playerObject == null && GameManager.instance.gameState != GameState.INTRO
			  && GameManager.instance.gameState != GameState.MAIN_MENU
			  && GameManager.instance.gameState != GameState.OPTIONS) {

			playerObject = GameObject.Find("Player");	

			if (playerObject == null) {
				Debug.Log("ERROR (PlayerManager.cs): \"Player)\" is not in scene.");
			}

			// Link up the character to the Main Camera so it can be followed.
			GameObject camera = GameObject.Find("Main Camera");
			if (camera == null)
				Debug.Log("ERROR (PlayerManager.cs): No \"Main Camera\" found.");
				
			if (GameManager.instance.gameState == GameState.OVERWORLD)
				GameObject.Find("Main Camera").GetComponent<CameraController>().followTarget = playerObject;
			else
				GameObject.Find("Main Camera").GetComponent<CameraController>().followTarget = 
				GameObject.Find("Main Camera").transform.Find("Dummy Camera Target").gameObject;
		}
	}

	void setupForNewScene () {
		// Stop player movement and hide sprite if they shouldn't be visible
		// TODO
		if (GameManager.instance.gameState == GameState.OVERWORLD) {
			// Enable movement
			playerObject.GetComponent<PlayerController>().enablePlayerMovement();
		}

		else if (GameManager.instance.gameState == GameState.INTRO
			  || GameManager.instance.gameState == GameState.MAIN_MENU
			  || GameManager.instance.gameState == GameState.OPTIONS) {
			// Do nothing
		}

		else if (GameManager.instance.gameState == GameState.BATTLE) {
			// Disable movement
			playerObject.GetComponent<PlayerController>().stopPlayerMovement();
		}

		else {
			Debug.Log("ERROR (PlayerManager.cs): What we doin in a weird game state? This isn't normal.");
		}
	}

}
