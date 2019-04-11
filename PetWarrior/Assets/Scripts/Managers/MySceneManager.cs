using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

	public static MySceneManager instance = null;

	public delegate void OnSceneChangeHandler();
	public static event OnSceneChangeHandler OnSceneChange;

	// Battle scene saving variables
	public string lastOverworldScene;

	void Awake() {
		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "Scene Man";
		}

		// Assign events
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
	}

	
	// Whenever scene is loaded, the event OnSceneChange is triggered
	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

		if (OnSceneChange != null) {
			GameManager.instance.onSceneChangePrep();
			OnSceneChange();
		}
	}

	// Loads a battle scene while saving important variables about the player
	public void loadBattleScene() {
		
		// Save current scene. Player direction and player position should stay the same, I'm pretty sure.
		lastOverworldScene = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene("battleScreen");
	}

}