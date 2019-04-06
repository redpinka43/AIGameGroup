using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveThreeClicked : MonoBehaviour
{
    public Button moveThree;
    public turnCheck turnCheck;
    private Pets playerPet;
    // Use this for initialization
    void Start()
    {
        moveThree.onClick.AddListener(moveThree_Click);
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        turnCheck = GameObject.Find("BattleManager").GetComponent<turnCheck>();
    }


    public void moveThree_Click()
    {
        playerPet.moveNum = 2;
        turnCheck.speedCheck();
    }
}
