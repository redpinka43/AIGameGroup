using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loader : MonoBehaviour 
{
	public GameObject gameManPrefab;	// GameManager prefab to instantiate.


	void Awake ()
	{	
		// Initialize Gamemanager
		// GameManager gm = GameManager.instance;
		

		// Okay we need somehow load in all the managers. And we don't want to use the
		// singleton class, cuz they need a prefab attached, I think.

		// We need to have Loader make sure GameManager and all of its components are ready
		// to go at the start of each scene. 
		
		// So far, Loader properly loads... hmm.
		// It does NOT properly load the GameManager prefab, *apparently*
		// What we need to do, is make sure the GameManager doesn't have any 
		// Instantiate(gameManPrefab);

		// // We need to replace the game manager with the prefab we just made
		// if (GameObject.Find("Game Manager") == null)
		// 	//Instantiate gameManager prefab
        //     Instantiate(gameManPrefab);

		
		if (GameManager.instance == null) {
                // Instantiate gameManager prefab
                Instantiate(gameManPrefab);
		}
	}
}
