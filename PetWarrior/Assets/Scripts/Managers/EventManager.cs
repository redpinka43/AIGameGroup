using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Not actually for all events in the game. I'm not even sure if this is the right thing. Perhaps it's referring
// more to GAME events, like plot progression. Hmm...
public class EventManager : MonoBehaviour {
	
	public static EventManager instance = null;

	public static bool haveBattledRivalInPetstore;

	void Awake () {
		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "Event Man";
		}

		// Assign events
	}

}
