using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

	public static MySceneManager instance = null;

	public delegate void OnSceneChangeHandler();
	public static event OnSceneChangeHandler OnSceneChange;

	public List<GameObject> destroyableObjects;
	public List<string> destroyableObjectsSourceScene;

	// Battle scene saving variables
	public string lastOverworldScene;
	public Vector2 lastFacingDirection;

	void Awake() {
		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "Scene Man";
		}

		// Assign events
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void Start() {

		// Assigne lastOverworldScene if it's null 
		if ( string.IsNullOrEmpty(lastOverworldScene) ) {
			lastOverworldScene = "town_1_petStore";
		}

		destroyableObjects = new List<GameObject>();
	}

	// Whenever scene is loaded, the event OnSceneChange is triggered
	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

		if (OnSceneChange != null) {
			GameManager.instance.onSceneChangePrep();
			OnSceneChange();
		}

		if(GameManager.instance.gameState == GameState.OVERWORLD) {

			// Destroy any trainer objects added to the object list	
			for(int i = 0; i < destroyableObjects.Count; i++) {

				if(destroyableObjectsSourceScene[i] != SceneManager.GetActiveScene().name) {
					Destroy(destroyableObjects[i]);
				}	
			}
			
			destroyableObjects = new List<GameObject>();
			destroyableObjectsSourceScene = new List<string>();
		}
	}


	// Loads a battle scene while saving important variables about the player
	public void loadBattleScene() {
		
		// Saving bool about rival in petstore
		if(SceneManager.GetActiveScene().name == "town_1_petStore") {
			EventManager.haveBattledRivalInPetstore = true;
		}

		// Save current scene. Player direction and player position should stay the same, I'm pretty sure.
		lastOverworldScene = SceneManager.GetActiveScene().name;
		Debug.Log("lastOverworldScene = " + lastOverworldScene);
		PlayerController.instance.startPoint = "";
		
		// Save player facing position
		Animator anim = PlayerController.instance.GetComponent<Animator>();
		lastFacingDirection = new Vector2(anim.GetFloat("MoveX"), anim.GetFloat("MoveY"));

		SceneManager.LoadScene("battleScreen");
	}

}