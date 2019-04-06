using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveOneClicked : MonoBehaviour {
    public Button moveOne;
    public turnCheck turnCheck;
    private Pets playerPet;
    // Use this for initialization
    void Start () {
        moveOne.onClick.AddListener(moveOne_Click);
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        turnCheck = GameObject.Find("BattleManager").GetComponent<turnCheck>();
    }

    public void moveOne_Click()
    {
        playerPet.moveNum = 0;
        turnCheck.speedCheck();
    }
}
