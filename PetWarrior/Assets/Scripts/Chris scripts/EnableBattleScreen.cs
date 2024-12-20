﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBattleScreen : MonoBehaviour {

    private GameObject battle;
    private GameObject mainCamera;
    private GameObject battleCamera;

    private PlayerController thePlayer;

    // Use this for initialization
    void Start () {
        thePlayer = FindObjectOfType<PlayerController>();

        battle = GameObject.Find("Battle");
        mainCamera = GameObject.Find("Main Camera");
        battleCamera = GameObject.Find("Battle Camera");
 
        battle.SetActive(false);
        battleCamera.SetActive(false);
    }

    // Update is called once per frame
    public void startBattleScreen () {

        thePlayer.canMove = false;
        mainCamera.SetActive(false);
        battle.SetActive(true);
        battleCamera.SetActive(true);
        
       
    }

    public void endBattle()
    {
        battleCamera.SetActive(false);

        mainCamera.SetActive(true);
        thePlayer.canMove = true;
        battle.SetActive(false);
    }
}
