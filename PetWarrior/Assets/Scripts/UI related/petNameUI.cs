using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petNameUI : MonoBehaviour {

	public int i;
	Text myText;
	private Player player;
	public Pets playerPet;
	public Text name;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		
		// i indicated the placeholder for pet array
		playerPet = player.playerPets[i];
		
		myText = GetComponentInChildren<Text>();
		myText.text = playerPet.petName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
