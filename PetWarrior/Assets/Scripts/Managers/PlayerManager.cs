using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game States
public enum PlayerGenders
{
	DEFAULT, 	// Fall-back state, should never happen]
	GIRL,
	BOY
};

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance = null;

	public PlayerGenders playerGender;
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
	}

	// Use this for initialization
	void Start () {
		playerGender = PlayerGenders.DEFAULT;
	}

	void linkUp() {
		// Basically, if Gamestate = OVERWORLD, then we search the hierarchy 
		// for either Player (Boy) or Player (Girl).
		if (GameManager.instance.gameState == GameState.OVERWORLD) {
			if (playerGender == PlayerGenders.DEFAULT) {

				playerObject = GameObject.Find("Player (Girl)");	
				// If girl object isn't in scene, check for boy
				if (playerObject == null) {
					playerObject = GameObject.Find("Player (Boy)");	
				}			
				if (playerObject == null) {
					Debug.Log("ERROR (PlayerManager.cs): Neither \"Player (Girl)\" or \"Player (Boy)\" are in scene.");
				}

				// Link up the character to the Main Camera so it can be followed.
				GameObject camera = GameObject.Find("Main Camera");
				if (camera == null)
					Debug.Log("ERROR (PlayerManager.cs): No \"Main Camera\" found.");
					
				GameObject.Find("Main Camera").GetComponent<CameraController>().followTarget = playerObject;
				
			}
		}
	}

}
