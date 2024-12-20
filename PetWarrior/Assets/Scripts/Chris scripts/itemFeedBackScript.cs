﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class itemFeedBackScript : MonoBehaviour
{

    Text txt;
    public string itemName;
    private Player player;
    private Pets playerPet;
    private Pets enemyPet;
    private useItem useItem;

    public bool callFlag = false;

    private void Awake()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();

        player = GameObject.Find("Player").GetComponent<Player>();
        useItem = GameObject.Find("useItemButton").GetComponent<useItem>();

        itemName = useItem.itemName;
    }

    private void OnEnable()
    {
        callFlag = false;
    }
    void Start()
    {
        txt = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        itemName = useItem.itemName;
        if (callFlag == false)
        {
            if (itemName == "Potion")
                txt.text = "You healed " + playerPet.petName + " for 10 health points! Woopdie doo!";
            if (itemName == "Antidote")
                txt.text = "This item doesn't do anything. You weren't even poisoned. Why are you like this?";
            if (itemName == "Gun")
            {
                if(enemyPet.owned == true)
                {
                    txt.text = "Woah! You can't just shoot someone elses pet! Not only is it illegal it's also not very fair...";
                }
                else
                {
                    string upper = playerPet.animal.ToUpper();
                    txt.text = " BLAT. BLAT. A " + upper + " WITH A GAT";
                }
                
            }
                

            callFlag = true;
        }
    }
}

