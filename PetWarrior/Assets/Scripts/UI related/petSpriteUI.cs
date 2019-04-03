using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petSpriteUI : MonoBehaviour {
	
	Image myImage;
	public int i;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		playerPet = player.playerPets[i]
		
		
		myImage = GetComponent<Image>();
        myImage.sprite = playerPet.image;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
