using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// by meaghan
public class petSpriteUI : MonoBehaviour {
	
	public int i;
	Image myImage;
	private Player player;
	private Pets playerPet;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		
		// i indicates the placeholder for pet array
		playerPet = player.playerPets[i];
		
		myImage = GetComponent<Image>();
        myImage.sprite = playerPet.panelImage;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
