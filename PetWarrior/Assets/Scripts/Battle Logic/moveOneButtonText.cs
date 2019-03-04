using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class moveOneButtonText : MonoBehaviour
{
	string moveName;
	int ppLeft = 30;
	int ppTotal = 30;
    private Pets playerPet;
    private Pets enemyPet;
    Text txt;
    

    private void Awake ()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();

    }
    void Start()
    {
        moveName = playerPet.moves[0];
        txt = GetComponentInChildren<Text>();
		txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
	}

    public void useMove(string move)
    {
        if (moveName == "Nip")
            Nip();
        if (moveName == "Growl")
            Growl();

       
    }
 
    public int Nip()
    {
        int damage = (playerPet.attack / enemyPet.defense);
        enemyPet.currentHealth -= damage;
        return damage;
    }


    public void Growl()
    {

    }
}
