using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// by meaghan
public class petHealthUI : MonoBehaviour {
	
	[SerializeField] private float fillAmount;

    [SerializeField] private Image filler;

	public int i;
	private Pets playerPet;
	private Player player;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// i indicates the placeholder for pet array
		playerPet = player.playerPets[i];
		HandleBar();
	}
	
	// updates how far the health bar should be filled
	private void HandleBar()
	{
		// current health / health because this will give a decimal to the fill bar
        // typecasted as floats to get decimals
		fillAmount = (float)playerPet.currentHealth / (float)playerPet.health;
        if (fillAmount != filler.fillAmount)
        {
            filler.fillAmount = fillAmount;
        }
	}
}
