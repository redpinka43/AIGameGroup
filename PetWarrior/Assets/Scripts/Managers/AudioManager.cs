using System;
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

	// Singleton instance
	public static AudioManager instance = null;

	// Child components
	private GameObject bgMusicObject;
	private GameObject soundFxObject;

	// Background music list
	public AudioClip[] bgMusicArray;
	public string[] bgMusicStrings;

	// Sound effects lists
	public AudioClip[] soundFxArray;
	public string[] soundFxStrings;

	// Class variables
	private string currentBgMusic = null;
	public float bgMusicVolume;
	public float soundFxVolume;


	void Awake () {

		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "Audio Man";
		}

		// Assign child components
		bgMusicObject = gameObject.transform.Find("bgMusic").gameObject;
		soundFxObject = gameObject.transform.Find("soundFx").gameObject;

		// Assign events
		MySceneManager.OnSceneChange += checkSceneChange;

	}

	// Use this for initialization
	void Start () {
		
		// Set volumes
		bgMusicObject.GetComponent<AudioSource>().volume = bgMusicVolume;
		soundFxObject.GetComponent<AudioSource>().volume = soundFxVolume;

	}	

	// New scene function. Changes background music 
	void checkSceneChange () {

		string newBgMusic = GameObject.Find("Scene Info").GetComponent<SceneInfo>().bgMusic;
		if (newBgMusic != currentBgMusic) {
			currentBgMusic = newBgMusic;
			// Change bg music
			changeBgMusic(newBgMusic);
		}

	}

	// Changes background music to the music matching the string newBgMusic 
	public void changeBgMusic(string newBgMusicString) {

		AudioClip newMusic = bgMusicArray[ Array.IndexOf(bgMusicStrings, newBgMusicString) ];			
		bgMusicObject.GetComponent<AudioSource>().clip = newMusic;
		// Idk do i need to activate the music now?
		bgMusicObject.GetComponent<AudioSource>().Play();

	}

	// Plays a sound effect matching the string newSoundFx
	void activateSoundFx(string newSoundFxString) {
		
		AudioClip newSoundFx = soundFxArray[ Array.IndexOf(soundFxStrings, newSoundFxString) ];
		soundFxObject.GetComponent<AudioSource>().clip = newSoundFx;
		// Idk do i need to activate the music now?

	}

	// Update is called once per frame
	void Update () {
		
	}
}
