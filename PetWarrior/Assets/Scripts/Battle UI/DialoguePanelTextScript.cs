using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialoguePanelTextScript : MonoBehaviour
{

    Text txt;
    public string moveName;
    private Pets enemyPet;
    private Pets playerPet;
    public int ppLeft;
    public string feedBackString;
    public static int moveNumberUsed;
    public moveOneButtonText moveOne;
    public moveOneButtonText moveTwo;
    public moveOneButtonText moveThree;
    public moveOneButtonText moveFour;

    public bool callFlag = false;

    private void Awake()
    {

        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        moveOne = GameObject.Find("moveOneButtonTextObject").GetComponent<moveOneButtonText>();
        moveNumberUsed = playerPet.moveNum;
        moveName = playerPet.moves[moveNumberUsed];
        Debug.Log(moveName);

        ppLeft = moveOne.ppLeft;
        

    }

    public void moveNumOne()
    {
        playerPet.moveNum = 0;
    }
    public void moveNumTwo()
    {
        playerPet.moveNum = 1;

    }
    public void moveNumThree()
    {
        playerPet.moveNum = 2;


    }
    public void moveNumFour()
    {
        playerPet.moveNum = 3;

    }

    private void OnEnable()
    {
        callFlag = false;
    }
    void Start()
    {
        txt = GetComponentInChildren<Text>();
        txt.text = "life left is " + enemyPet.currentHealth + " click to continue...";

        //feedBackPanel.SetActive(false);
    }

    private void Update()
    {
        moveNumberUsed = playerPet.moveNum;
        moveName = playerPet.moves[moveNumberUsed];
        Debug.Log(moveName);
        if (ppLeft == 0)
        {
            txt.text = "Sorry bud, you can't use that move.";
        }
        else
        {
            if (callFlag == false)
            {
                if (moveName == "Nip")
                    feedBackString = playerPet.name + " nipped the opponent for " + moveOne.Nip() + " damage";
                if (moveName == "Dance")
                    feedBackString = playerPet.name + " did a little dance! Enemy Defense lowered by: " + moveOne.Dance();
                if (moveName == "Sticky Slap")
                {
                    int ssDam = moveOne.StickySlap();
                    if(ssDam != 0)
                    {
                        feedBackString = playerPet.name + " dealt " + ssDam + " damage. Oh god, what was on it's hand?";
                    }
                    else
                    {
                        feedBackString = playerPet.name + "'s Sticky Slap missed! Bet that feels bad.";
                    }
                    
                }
                if(moveName == "Shed Skin")
                {
                    feedBackString = playerPet.name + " feels restored!";
                }

                txt.text = feedBackString;
                callFlag = true;
            }
        }
    }
}

   

