using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
public class moveOneButtonText : MonoBehaviour
{
    string moveName;
    public int ppLeft = 30;
    public int ppTotal = 30;
    private Pets playerPet;
    private Pets enemyPet;
    Text txt;


    private void Awake()
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

    private void Update()
    {
        // avoids division by 0
        if (enemyPet.defense == 0)
            enemyPet.defense = 1;

        txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
    }

    public void useMove(string move)
    {
        moveName = playerPet.moves[playerPet.moveNum];
        if (ppLeft == 0)
        {
            return;
        }

        switch (moveName)
        {
            case "Nip":
                enemyPet.currentHealth -= Nip();
                break;
            case "Dance":
                enemyPet.defense -= Dance();
                break;
            case "Sticky Slap":
                break;
            case "Shed Skin":
                ShedSkin();
                break;
            case "Shell":
                break;
            case "Growl":
                break;
            case "Acorn Throw":
                break;
            case "Fury Swipes":
                break;
            default:
                Debug.Log("No such move");
                break;
        }
        ppLeft--;



    }

    public int Nip()
    {
        int damage = (int)(Math.Ceiling((double)enemyPet.attack / (double)playerPet.defense));

        return damage;
    }


    public int Dance()
    {
        int val;
        if (enemyPet.defense <= 10)
            val = 1;
        else
        {
            val = (enemyPet.defense / 10);
        }
        return val;
    }

    public int StickySlap()
    {
        int rand = RNG(1, 100);
        int damage;
        if (rand <= 75)
        {
            damage = (playerPet.attack + 10) / playerPet.defense;
        }
        else
        {
            damage = 0;

        }
        enemyPet.currentHealth -= damage;

        return damage;
    }

    public void ShedSkin()
    {
        if (playerPet.currentHealth <= (playerPet.health - 5))
        {
            playerPet.currentHealth += 5;
        }
        else
        {
            playerPet.currentHealth = playerPet.health;
        }

        playerPet.attack = playerPet.maxAttack;
        playerPet.defense = playerPet.maxDefense;


    }

    public int Shell()
    {
        int boost = (int)Math.Ceiling((double)playerPet.defense * .2);
        playerPet.defense += boost;
        return boost;
    }

    public int Growl()
    {
        int val = (int)Math.Ceiling((double)enemyPet.defense * .1);
        enemyPet.defense -= val;
        return val;
    }
    int RNG(int min, int max)
    {
        int num;
        num = UnityEngine.Random.Range(min, max + 1);
        return num;
    }
}
