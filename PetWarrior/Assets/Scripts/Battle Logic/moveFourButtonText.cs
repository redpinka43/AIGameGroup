﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveFourButtonText : MonoBehaviour
{
    string moveName;
    public int ppLeft;
    public int ppTotal;
    private Pets playerPet;
    private Pets enemyPet;
    Text txt;
    public Button moveFourButton;


    private void Awake()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();

    }
    void Start()
    {
        moveName = playerPet.moves[3].moveName;
        ppLeft = playerPet.moves[3].ppCurrent;
        ppTotal = playerPet.moves[3].ppMax;

     
        txt = GetComponentInChildren<Text>();
        txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
    }

    private void Update()
    {
        // can't click if 0 pp
        if (ppLeft == 0)
        {
            moveFourButton.interactable = false;
        }
        else
            moveFourButton.interactable = true;

        // avoids division by 0
        if (enemyPet.defense == 0)
            enemyPet.defense = 1;

        txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
    }

    public void useMove()
    {
        ppLeft = playerPet.moves[playerPet.moveNum].ppCurrent;

        if (ppLeft == 0)
        {
            Debug.Log("ovetwopp=0");
            return;
        }

        ppLeft--;




    }

    public int Nip()
    {
        int damage = (playerPet.attack / enemyPet.defense);

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


    int RNG(int min, int max)
    {
        int num;
        num = UnityEngine.Random.Range(min, max + 1);
        return num;
    }
}