using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class moveFourClicked : MonoBehaviour {

    public Button moveFour;
    public turnCheck turnCheck;
    private Pets playerPet;
    // Use this for initialization
    void Start()
    {
        moveFour.onClick.AddListener(moveFour_Click);
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        turnCheck = GameObject.Find("BattleManager").GetComponent<turnCheck>();
    }


    public void moveFour_Click()
    {
        playerPet.moveNum = 3;
        turnCheck.speedCheck();
    }
}
