using System;
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

    public Button feedBackTextButton;
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
        if (ppLeft == 0)
        {
            txt.text = "Sorry bud, you can't use that move.";
        }
        else
        {
            if (callFlag == false)
            {
                if (moveName == "Nip")
                    txt.text = playerPet.name + " nipped the opponent for " + moveOne.Nip() + " damage";
                if (moveName == "Dance")
                    txt.text = playerPet.name + " did a little dance! Enemy Defense lowered by: " + moveOne.Dance();
                if (moveName == "Sticky Slap")
                {
                    int ssDam = moveOne.StickySlap();
                    if(ssDam != 0)
                    {
                        txt.text = playerPet.name + " dealt " + ssDam + " damage. Oh god, what was on it's hand?";
                    }
                    else
                    {
                        txt.text = playerPet.name + "'s Sticky Slap missed! Bet that feels bad.";
                    }
                    
                }
                if (moveName == "Shed Skin")
                {
                    txt.text = playerPet.name + " feels restored!";
                }
                if (moveName == "Shell")
                {
                    txt.text = playerPet.name + " hid inside it's shell! It's defense rose by " + moveOne.Shell();
                }
                if (moveName == "Growl")
                {
                    txt.text = playerPet.name + " growled at " +enemyPet.name +" it's attack decreased by " +moveOne.Growl();
                }
                if (moveName == "Hiss")
                {
                    txt.text = playerPet.name + " hissed at " + enemyPet.name + " it's attack decreased by " + moveOne.Growl();
                }
                if (moveName == "Acorn Throw")
                {
                    AcornThrow();
                }
                if (moveName == "Fury Swipes")
                {
                    FurySwipes();
                }
                

                callFlag = true;
            }
        }
    }

    public void AcornThrow()
    {
        StartCoroutine(GamePauserAT());
    }
    public IEnumerator GamePauserAT()
    {
        int damage = (int)(Math.Ceiling((double)enemyPet.attack / (double)playerPet.defense));
        int total = 0;
        feedBackTextButton.interactable = false;


        txt.text = playerPet.name + " begins throwing acorns!";

        // first acorn
        yield return new WaitForSeconds(1.0f);

        if (RNG(1, 2) == 1)
        {
            txt.text = playerPet.name + " 's First acorn dealt " + damage + " damage!";
            enemyPet.currentHealth -= damage;
            total += damage;
        }

        else
            txt.text = playerPet.name + " 's First acorn missed!";

        if (enemyPet.currentHealth < 1)
        {
            feedBackTextButton.interactable = true;
            yield break;
        }

        // second acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = playerPet.name + " 's Second acorn dealt " + damage + " damage!";
            enemyPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = playerPet.name + " 's Second acorn missed!";

        if (enemyPet.currentHealth < 1)
        {
            feedBackTextButton.interactable = true;
            yield break;
        }

        // third acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = playerPet.name + " 's Third acorn dealt " + damage + " damage!";
            enemyPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = playerPet.name + " 's Third acorn missed!";

        if (enemyPet.currentHealth < 1)
        {
            feedBackTextButton.interactable = true;
            yield break;
        }

        // fourth acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = playerPet.name + " 's Fourth acorn dealt " + damage + " damage!";
            enemyPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = playerPet.name + " 's Fourth acorn missed!";

        // total damage
        yield return new WaitForSeconds(1.0f);
        txt.text = playerPet.name + " dealt " + total + " total damage!";

        yield return new WaitForSeconds(.5f);
        feedBackTextButton.interactable = true;

    }


    public void FurySwipes()
    {
        StartCoroutine(GamePauserFS());
    }
    public IEnumerator GamePauserFS()
    {
        int damage = (int)(Math.Ceiling((double)enemyPet.attack / (double)playerPet.defense));
        int total = 0;
        feedBackTextButton.interactable = false;


        txt.text = playerPet.name + " begins throwing swiping with it's claws!";

        // first acorn
        yield return new WaitForSeconds(1.0f);

        if (RNG(1, 2) == 1)
        {
            txt.text = playerPet.name + " 's First swipe dealt " + damage + " damage!";
            enemyPet.currentHealth -= damage;
            total += damage;
        }

        else
            txt.text = playerPet.name + " 's First swipe missed!";

        if (enemyPet.currentHealth < 1)
        {
            feedBackTextButton.interactable = true;
            yield break;
        }

        // second acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = playerPet.name + " 's Second swipe dealt " + damage + " damage!";
            enemyPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = playerPet.name + " 's Second swipe missed!";

        if (enemyPet.currentHealth < 1)
        {
            feedBackTextButton.interactable = true;
            yield break;
        }

        // third acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = playerPet.name + " 's Third swipe dealt " + damage + " damage!";
            enemyPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = playerPet.name + " 's Third swipe missed!";

        if (enemyPet.currentHealth < 1)
        {
            feedBackTextButton.interactable = true;
            yield break;
        }

        // fourth acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = playerPet.name + " 's Fourth swipe dealt " + damage + " damage!";
            enemyPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = playerPet.name + " 's Fourth swipe missed!";

        // total damage
        yield return new WaitForSeconds(1.0f);
        txt.text = playerPet.name + " dealt " + total + " total damage!";

        yield return new WaitForSeconds(.5f);
        feedBackTextButton.interactable = true;

    }

    int RNG(int min, int max)
    {
        int num;
        num = UnityEngine.Random.Range(min, max + 1);
        return num;
    }
}

   

