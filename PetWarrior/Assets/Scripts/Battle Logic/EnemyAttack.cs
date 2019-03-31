using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{

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
        moveName = enemyPet.moves[0];
        moveOne = GameObject.Find("moveOneButtonTextObject").GetComponent<moveOneButtonText>();


    }
    void Start()
    {
        txt = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        int dam = enemyPet.attack / playerPet.defense;

        

        if (moveName == "Nip")
            feedBackString = enemyPet.name + " nipped " + playerPet.name + " for " + dam + " damage! ";

        

       txt.text = feedBackString;
    }

    public void useMove(string move)
    {
        if (moveName == "Nip" && enemyPet.currentHealth >= 1)
            Nip();

    }

    public int Nip()
    {
        int damage = (enemyPet.attack / playerPet.defense);
        playerPet.currentHealth -= damage;
        return damage;
    }

}
