using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using E7.Introloop;

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
	public IntroloopAudio[] bgMusicArray;
	public string[] bgMusicStrings;

	// Sound effects lists
	public IntroloopAudio[] soundFxArray;
	public string[] soundFxStrings;

	// Class variables
	private string currentBgMusicStr = null;
	public float bgMusicVolume;
	public float soundFxVolume;
	public bool currentMusicHasIntro;

	public IntroloopAudio currentBgMusic;

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

	// New scene function. Changes background music when scene is changed
	void checkSceneChange () {

		string newBgMusic = GameObject.Find("Scene Info").GetComponent<SceneInfo>().bgMusic;
		// Change bg music
		changeBgMusic(newBgMusic);


	}

	// Changes background music to the music matching the string newBgMusic. 
	public void changeBgMusic(string newBgMusicString) {

		// Only change to new music
		if (newBgMusicString != currentBgMusicStr) {
			
			currentBgMusicStr = newBgMusicString;

			currentBgMusic = bgMusicArray[ Array.IndexOf(bgMusicStrings, newBgMusicString) ];			
			IntroloopPlayer.Instance.Play(currentBgMusic);

		}

	}

	// Plays a sound effect matching the string newSoundFx
	void activateSoundFx(string newSoundFxString) {
		
		IntroloopAudio newSoundFx = soundFxArray[ Array.IndexOf(soundFxStrings, newSoundFxString) ];
		// soundFxObject.GetComponent<AudioSource>().clip = newSoundFx;
		// Idk do i need to activate the music now?

	}

}
