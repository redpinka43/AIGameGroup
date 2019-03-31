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
    public moveOneButtonText moveOne;

   public bool callFlag = false;

    private void Awake()
    {

        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        moveName = playerPet.moves[0];

        moveOne = GameObject.Find("moveOneButtonTextObject").GetComponent<moveOneButtonText>();
        ppLeft = moveOne.ppLeft;
        

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
                    feedBackString = playerPet.name + " dealt " + moveOne.StickySlap() + " damage. Oh god, what was on it's hand?";

                txt.text = feedBackString;
                callFlag = true;
            }
        }
    }
}

   

