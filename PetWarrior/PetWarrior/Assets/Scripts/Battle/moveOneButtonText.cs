using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveOneButtonText : MonoBehaviour
{
	Text txt;
	string moveName;
	int ppLeft = 30;
	int ppTotal = 30;
    private Pets playerPet;
    private Pets enemyPet;
    

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
        enemyPet.currentHealth -= 1;

        Debug.Log("e: " + enemyPet.currentHealth);
        Debug.Log("p: " + playerPet.currentHealth);
    }
}
