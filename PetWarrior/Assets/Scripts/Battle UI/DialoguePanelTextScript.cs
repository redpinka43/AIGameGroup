using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelTextScript : MonoBehaviour {

    Text txt;
    public string moveName;
    private Pets enemyPet;
    private Pets playerPet;
    public string feedBackString;
    private moveOneButtonText moveOne;

    private void Awake()
    {
        
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        moveName = playerPet.moves[0];
        moveOne = GameObject.Find("moveOneButtonTextObject").GetComponent<moveOneButtonText>(); 
        

    }
    void Start()
    {
        txt = GetComponentInChildren<Text>();
        txt.text = "life left is " + enemyPet.currentHealth + " click to continue...";
       
        
        //feedBackPanel.SetActive(false);
    }

    private void Update()
    {
        
        int dam = playerPet.attack / enemyPet.defense;
        if (moveName == "Nip")
            feedBackString = playerPet.name + " nipped the opponent for " + dam + " damage";
        txt.text = feedBackString;
    }

   
}
