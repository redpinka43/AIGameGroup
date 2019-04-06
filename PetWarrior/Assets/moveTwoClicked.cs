using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveTwoClicked : MonoBehaviour {
    public Button moveTwo;
    public turnCheck turnCheck;
    private Pets playerPet;
    // Use this for initialization
    void Start()
    {
        moveTwo.onClick.AddListener(moveTwo_Click);
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        turnCheck = GameObject.Find("BattleManager").GetComponent<turnCheck>();
    }

    public void moveTwo_Click()
    {
        playerPet.moveNum = 1;
        turnCheck.speedCheck();
    }
}
