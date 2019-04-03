using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petSpriteUI : MonoBehaviour {
	
	public int i;
	Image myImage;
	private Player player;
	public Pets playerPet;
	public Sprite thisImage;
    public Sprite image;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		playerPet = player.playerPets[i];
		
		
		myImage = GetComponent<Image>();
        myImage.sprite = playerPet.image;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
