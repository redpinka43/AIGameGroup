﻿using System;
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
    private int damage;
    public string feedBackString;
    public static int moveNumberUsed;
    public moveOneButtonText moveOne;
    public moveOneButtonText moveTwo;
    public moveOneButtonText moveThree;
    public moveOneButtonText moveFour;

    public Button feedBackTextButton;
    public bool callFlag = false;
    private bool hasStatus = false;
    private void Awake()
    {

        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        moveOne = GameObject.Find("moveOneButtonTextObject").GetComponent<moveOneButtonText>();
        moveNumberUsed = playerPet.moveNum;
        moveName = playerPet.moves[moveNumberUsed];
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
        txt.text = "";

        //feedBackPanel.SetActive(false);
    }

    public void CheckStatus()
    {

        foreach (StatusEffects status in playerPet.statusEffects)
        {
            if (status.effectType == "Cringe")
            {

                status.endAfter--;
                if (status.endAfter < 1)
                {
                    moveName = "";
                    txt.text = playerPet.name + " completely forgets why it was cringing.";
                    StatusRemoved();
                    playerPet.statusEffects.Remove(status);
                    break;
                }

                if (status.endAfter >= 1 && (RNG(1, 2) == 2))
                {
                    moveName = "";
                    txt.text = "Oh god..." + playerPet.name + " remembers the cringe. It can't move!";
                    StatusEffectOccurs();
                }
            }
        }

    }

    public void StatusEffectOccurs()
    {
        StartCoroutine(StatusFeedback(2));
    }
    public void StatusRemoved()
    {
        StartCoroutine(StatusFeedback(3));
    }
    public IEnumerator StatusFeedback(int val)
    {
        if (val == 2)
        {
            feedBackTextButton.interactable = false;

            yield return new WaitForSeconds(1.0f);

            feedBackTextButton.interactable = true;
        }
        if (val == 3)
        {
            feedBackTextButton.interactable = false;

            yield return new WaitForSeconds(1.5f);
            callFlag = false;

            feedBackTextButton.interactable = true;
        }
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
                callFlag = true;

                moveNumberUsed = playerPet.moveNum;
                moveName = playerPet.moves[moveNumberUsed];

                CheckStatus();


                if (moveName == "Nip")
                {
                    damage = moveOne.Nip();
                    enemyPet.currentHealth -= damage;
                    txt.text = playerPet.name + " nipped the opponent for " + damage + " damage.";


                }

                if (moveName == "Dance")
                {
                    // damage is just an int value in this context
                    damage = moveOne.Dance();
                    enemyPet.defense -= damage;
                    txt.text = playerPet.name + " did a little dance! Enemy Defense lowered by: " + damage;
                }
                if (moveName == "Sticky Slap")
                {
                    int ssDam = moveOne.StickySlap();
                    if (ssDam != 0)
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
                    moveOne.ShedSkin();
                    txt.text = playerPet.name + " feels restored!";
                }
                if (moveName == "Shell")
                {
                    txt.text = playerPet.name + " hid inside it's shell! It's defense rose by " + moveOne.Shell();
                }
                if (moveName == "Growl")
                {
                    txt.text = playerPet.name + " growled at " + enemyPet.name + " it's attack decreased by " + moveOne.Growl();
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

                if (moveName == "Cringe")
                {
                    // already has cringe effect
                    foreach (StatusEffects status in enemyPet.statusEffects)
                    {
                        if (status.effectType == "Cringe")
                        {
                            txt.text = "Woah there, your opponent is already cringing hard enough. Give them a break for God's sake. ";
                            hasStatus = true;
                        }
                    }

                    if(hasStatus == false)
                    {

                    moveOne.Cringe();
                    CringeText();
                    }

                    hasStatus = false;
                }



                if (playerPet.hasAdvanatage == true)
                {

                    txt.text += "\n\n<color=green>(btw..." + playerPet.animal + "s are obviously strong against " + enemyPet.animal + "s " + "so that attack dealt more damage than usual.)</color>";
                    playerPet.hasAdvanatage = false;

                }
                if (playerPet.hasDisadvantage == true)
                {

                    txt.text += "\n\n<color=red>(btw..." + playerPet.animal + "s are obviously weak against " + enemyPet.animal + "s " + "so that attack dealt less damage than usual.)</color>";
                    playerPet.hasDisadvantage = false;

                }



            }
        }
    }

    public void CringeText()
    {
        int rngval = RNG(1, 4);


        switch (rngval)
        {
            case (1):
                txt.text = playerPet.name + " says \"#all lives matter\"...\n\n";
                break;
            case (2):
                txt.text = playerPet.name + " reminds " + enemyPet.name + " they responded \"you too\" to a waiter that said to enjoy their meal.\n\n";
                break;
            case (3):
                txt.text = playerPet.name + " tells a bad joke. It wasn't even close to being funny.\n\n";
                break;
            case (4):
                txt.text = playerPet.name + " says that they honestly believe the moon landing was faked.\n\n";
                break;
            default:
                break;

        }

        txt.text += enemyPet.name + " can't help but cringe for a few turns.";


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
        damage = moveOne.AnimalAdvantage(damage);


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

        if (playerPet.hasAdvanatage == true)
        {

            txt.text += "\n\n<color=green>(btw..." + playerPet.animal + "s are obviously strong against " + enemyPet.animal + "s " + "so that attack dealt more damage than usual.)</color>";
            playerPet.hasAdvanatage = false;

        }
        if (playerPet.hasDisadvantage == true)
        {

            txt.text += "\n\n<color=red>(btw..." + playerPet.animal + "s are obviously weak against " + enemyPet.animal + "s " + "so that attack dealt less damage than usual.)</color>";
            playerPet.hasDisadvantage = false;

        }
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
        damage = moveOne.AnimalAdvantage(damage);
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
        if (playerPet.hasAdvanatage == true)
        {

            txt.text += "\n\n<color=green>(btw..." + playerPet.animal + "s are obviously strong against " + enemyPet.animal + "s " + "so that attack dealt more damage than usual.)</color>";
            playerPet.hasAdvanatage = false;

        }
        if (playerPet.hasDisadvantage == true)
        {

            txt.text += "\n\n<color=red>(btw..." + playerPet.animal + "s are obviously weak against " + enemyPet.animal + "s " + "so that attack dealt less damage than usual.)</color>";
            playerPet.hasDisadvantage = false;

        }

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



