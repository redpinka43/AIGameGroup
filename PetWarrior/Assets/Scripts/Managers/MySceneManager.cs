using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

	public static MySceneManager instance = null;

	public delegate void OnSceneChangeHandler();
	public static event OnSceneChangeHandler OnSceneChange;

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

		if (OnSceneChange != null)
			GameManager.instance.onSceneChangePrep();
			OnSceneChange();
	}

}