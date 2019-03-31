using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public List<PlayerPets> playerPets = new List<PlayerPets>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class PlayerPets
{
    public string Name;
    public Pets pet;
    public int level;
}
