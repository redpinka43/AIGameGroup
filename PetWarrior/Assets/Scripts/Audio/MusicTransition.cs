using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransition : MonoBehaviour {

	public string newMusic;

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		if(other.gameObject == PlayerManager.instance.playerObject)
		{
			AudioManager.instance.changeBgMusic(newMusic);
		}
	}

}
