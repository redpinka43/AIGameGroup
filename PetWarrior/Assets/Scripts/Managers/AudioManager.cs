using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	AudioManager manages the following:
		- Background music for each area
		- Sound effects

	For background music, when a new scene is loaded, AudioManager checks if the
	SceneInfo's audio track is the same one that's playing. If it is, it continues playing.
	If it isn't, it switches the playing audio track to the new one.

	For sound effects, external classes call functions within AudioManager to activate 
	sound effects.

	AudioManager will have 2 child game objects, bgMusic and fxSound
 */


public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	void Awake () {
		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "Audio Man";
		}
	}

	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		
	}
}
