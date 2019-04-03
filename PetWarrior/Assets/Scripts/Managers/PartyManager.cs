using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

	// Singleton instance
	public static PartyManager instance = null;

	// Pet array
	public Pets[] petParty = new Pets[6];

	void Awake () {

		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "PetParty Man";
		}

	}

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void addPet(Pets newPet) {
		
	}

}
