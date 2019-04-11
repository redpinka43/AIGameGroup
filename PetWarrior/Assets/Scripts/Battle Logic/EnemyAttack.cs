﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{

    Text txt;
    public string moveName;
    private Pets enemyPet;
    private Pets playerPet;
    public string feedBackString;
    private moveOneButtonText moveOne;
    public Button enemyFeedBackTextButton;
    public StatusEffects effect;
    private bool hasStatus = false;
    public bool callFlag = false;
    private void Awake()
    {

        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        
        moveOne = GameObject.Find("moveOneButtonTextObject").GetComponent<moveOneButtonText>();


    }
    void Start()
    {
        txt = GetComponentInChildren<Text>();

    }
    private void OnEnable()
    {
        callFlag = false;
        moveName = enemyPet.moves[UnityEngine.Random.Range(0, 3)];

    }
    public void AvoidDuplicateStatus()
    {
        foreach (StatusEffects status in playerPet.statusEffects)
        {
            // the enemy is trying to use a status effect that already exists on the player pet
            if (status.effectType == moveName)
            {
                // look through the other moves, if it's NOT that duplicate effect, just use that instead
                for(int i = 0;i < enemyPet.moves.Count; i++)
                {
                    if (enemyPet.moves[i] != moveName)
                    {
                        moveName = enemyPet.moves[i];
                        break;
                    }
                }
            }
    }
   

        foreach (StatusEffects status in enemyPet.statusEffects)
        {
            if (status.effectType == "Cringe")
            {

                status.endAfter--;
                if (status.endAfter < 1)
                {
                    moveName = "";
                    txt.text = enemyPet.name + " completely forgets why it was cringing.";
                    StatusRemoved();
                    playerPet.statusEffects.Remove(status);
                    break;
                }

                if (status.endAfter >= 1 && (RNG(1, 2) == 2))
                {
                    moveName = "";
                    txt.text = "Oh god..." + enemyPet.name + " remembers the cringe. It can't move!";
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
            enemyFeedBackTextButton.interactable = false;

            yield return new WaitForSeconds(1.0f);

            enemyFeedBackTextButton.interactable = true;
        }
        if (val == 3)
        {
            enemyFeedBackTextButton.interactable = false;

            yield return new WaitForSeconds(1.5f);
            callFlag = false;

            enemyFeedBackTextButton.interactable = true;
        }
    }
    private void Update()
    {



        if (callFlag == false)
        {
            // checks that it doesn't try to pointlessly add a status effect twice
            AvoidDuplicateStatus();

            if (moveName == "Nip")
                txt.text = enemyPet.name + " nipped " + playerPet.name + " for " + Nip() + " damage! ";
            if (moveName == "Dance")
                txt.text = enemyPet.name + " did a little dance! Enemy Defense lowered by: " + Dance();
            if (moveName == "Sticky Slap")
            {
                int ssDam = StickySlap();
                if (ssDam != 0)
                {
                    txt.text = enemyPet.name + " dealt " + ssDam + " damage. Oh god, what was on it's hand?";
                }
                else
                {
                    txt.text = enemyPet.name + "'s Sticky Slap missed! Bet that feels bad.";
                }

            }
            if (moveName == "Shed Skin")
            {
                txt.text = enemyPet.name + " feels restored!";
            }
            if (moveName == "Acorn Throw")
            {
                Debug.Log("here");
                AcornThrow();
            }
            if (moveName == "Fury Swipes")
            {
                Debug.Log("here");

                FurySwipes();
            }
            if (moveName == "Shell")
            {
                txt.text = enemyPet.name + " hid inside it's shell! It's defense rose by " + Shell();
            }

            if (moveName == "Cringe")
            {
                // already has cringe effect
                foreach (StatusEffects status in playerPet.statusEffects)
                {
                    if (status.effectType == "Cringe")
                    {
                        txt.text = "The enemy " +enemyPet.name +" tries to make " +playerPet.name +" cringe, but " +playerPet.name +" is still cringing from the last time..";
                        hasStatus = true;
                    }
                }

                if (hasStatus == false)
                {

                    Cringe();
                    CringeText();
                }

                hasStatus = false;
            }
        }
        if (enemyPet.hasAdvanatage == true)
        {

            txt.text += "\n\n<color=green>(btw..." + enemyPet.animal + "s are obviously strong against " + playerPet.animal + "s " + "so that attack dealt more damage than usual.)</color>";
            enemyPet.hasAdvanatage = false;

        }
        if (enemyPet.hasDisadvantage == true)
        {

            txt.text += "\n\n<color=red>(btw..." + enemyPet.animal + "s are obviously weak against " + playerPet.animal + "s " + "so that attack dealt less damage than usual.)</color>";
            enemyPet.hasDisadvantage = false;

        }
        callFlag = true;
    }


    public int Nip()
    {
        int damage = (int)(Math.Ceiling((double)enemyPet.attack / (double)playerPet.defense));
        damage = AnimalAdvantage(damage);
        playerPet.currentHealth -= damage;
        return damage;
    }

    public int Dance()
    {
        int val;
        if (playerPet.defense <= 10)
            val = 1;
        else
        {
            val = (playerPet.defense / 10);
        }
        playerPet.defense -= val;

        return val;
    }

    public int Shell()
    {
        int boost = (int)Math.Ceiling((double)enemyPet.defense * .2);
        enemyPet.defense += boost;
        return boost;
    }

    public void Cringe()
    {
        effect = new StatusEffects();
        effect.effectType = "Cringe";
        effect.endAfter = 3;
        playerPet.statusEffects.Add(effect);

    }
    public void CringeText()
    {
        int rngval = RNG(1, 4);


        switch (rngval)
        {
            case (1):
                txt.text = enemyPet.name + " says \"#all lives matter\"...\n\n";
                break;
            case (2):
                txt.text = enemyPet.name + " reminds " + playerPet.name + " they responded \"you too\" to a waiter that said to enjoy their meal.\n\n";
                break;
            case (3):
                txt.text = enemyPet.name + " tells a bad joke. It wasn't even close to being funny.\n\n";
                break;
            case (4):
                txt.text = enemyPet.name + " says that they honestly believe the moon landing was faked.\n\n";
                break;
            default:
                break;

        }

        txt.text += playerPet.name + " can't help but cringe for a few turns.";


    }

    public int StickySlap()
    {
        int rand = RNG(1, 100);
        int damage;
        if (rand <= 75)
        {
            damage = (enemyPet.attack + 10) / enemyPet.defense;
        }
        else
        {
            damage = 0;

        }
        damage = AnimalAdvantage(damage);
        playerPet.currentHealth -= damage;

        return damage;
    }

    public void ShedSkin()
    {
        if (enemyPet.currentHealth <= (enemyPet.health - 5))
        {
            enemyPet.currentHealth += 5;
        }
        else
        {
            enemyPet.currentHealth = enemyPet.health;
        }

        enemyPet.attack = enemyPet.maxAttack;
        enemyPet.defense = enemyPet.maxDefense;


    }

    public void AcornThrow()
    {
        StartCoroutine(GamePauserAT());
    }
    public IEnumerator GamePauserAT()
    {
        int damage = (int)(Math.Ceiling((double)playerPet.attack / (double)enemyPet.defense));
        int total = 0;
        damage = AnimalAdvantage(damage);
        enemyFeedBackTextButton.interactable = false;


        txt.text = enemyPet.name + " begins throwing acorns!";
      
        // first acorn
        yield return new WaitForSeconds(1.0f);

        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " First acorn dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }

        else
            txt.text = enemyPet.name + " First acorn missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // second acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " Second acorn dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " Second acorn missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // third acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's Third acorn dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " 's Third acorn missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // fourth acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's Fourth acorn dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " 's Fourth acorn missed!";

        // total damage
        yield return new WaitForSeconds(1.0f);
        txt.text = enemyPet.name + " dealt " + total + " total damage!";
        if (enemyPet.hasAdvanatage == true)
        {

            txt.text += "\n\n<color=green>(btw..." + enemyPet.animal + "s are obviously strong against " + playerPet.animal + "s " + "so that attack dealt more damage than usual.)</color>";
            enemyPet.hasAdvanatage = false;

        }
        if (enemyPet.hasDisadvantage == true)
        {

            txt.text += "\n\n<color=red>(btw..." + enemyPet.animal + "s are obviously weak against " + playerPet.animal + "s " + "so that attack dealt less damage than usual.)</color>";
            enemyPet.hasDisadvantage = false;

        }
        yield return new WaitForSeconds(.5f);
        enemyFeedBackTextButton.interactable = true;

    }


    public void FurySwipes()
    {
        StartCoroutine(GamePauserFS());
    }
    public IEnumerator GamePauserFS()
    {
        int damage = (int)(Math.Ceiling((double)playerPet.attack / (double)enemyPet.defense));
        int total = 0;
        damage = AnimalAdvantage(damage);
        enemyFeedBackTextButton.interactable = false;


        txt.text = enemyPet.name + " begins throwing swiping with it's claws!";

        // first acorn
        yield return new WaitForSeconds(1.0f);

        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's First swipe dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }

        else
            txt.text = enemyPet.name + " 's First swipe missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // second acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's Second swipe dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " 's Second swipe missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // third acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's Third swipe dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " 's Third swipe missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // fourth acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's Fourth swipe dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " 's Fourth swipe missed!";

        // total damage
        yield return new WaitForSeconds(1.0f);
        txt.text = enemyPet.name + " dealt " + total + " total damage!";
        if (enemyPet.hasAdvanatage == true)
        {

            txt.text += "\n\n<color=green>(btw..." + enemyPet.animal + "s are obviously strong against " + playerPet.animal + "s " + "so that attack dealt more damage than usual.)</color>";
            enemyPet.hasAdvanatage = false;

        }
        if (enemyPet.hasDisadvantage == true)
        {

            txt.text += "\n\n<color=red>(btw..." + enemyPet.animal + "s are obviously weak against " + playerPet.animal + "s " + "so that attack dealt less damage than usual.)</color>";
            enemyPet.hasDisadvantage = false;

        }
        yield return new WaitForSeconds(.5f);
        enemyFeedBackTextButton.interactable = true;

    }



    int RNG(int min, int max)
    {
        int num;
        num = UnityEngine.Random.Range(min, max + 1);
        return num;
    }

    public int AnimalAdvantage(int damage)
    {
        int newVal = damage;
        int newValAdvantage = (int)Math.Ceiling((double)damage * 1.5);
        int newValDisadvantage = (int)Math.Ceiling((double)damage * 0.5);
        string pet2 = playerPet.animal;
        string pet1 = enemyPet.animal;

        if (pet1 == "Gecko" && pet2 == "Bird")
        {
            newVal = newValDisadvantage;
            enemyPet.hasDisadvantage = true;

        }
        if (pet1 == "Gecko" && pet2 == "Dog")
        {
            newVal = newValAdvantage;
            enemyPet.hasAdvanatage = true;

        }
        if (pet1 == "Gecko" && pet2 == "Cat")
        {
            newVal = newValDisadvantage;
            enemyPet.hasDisadvantage = true;

        }
        if (pet1 == "Turtle" && pet2 == "Bird")
        {
            newVal = newValAdvantage;
            enemyPet.hasAdvanatage = true;

        }
        if (pet1 == "Turtle" && pet2 == "Rat")
        {
            newVal = newValDisadvantage;
            enemyPet.hasDisadvantage = true;

        }
        if (pet1 == "Bird" && pet2 == "Gecko")
        {
            newVal = newValAdvantage;
            enemyPet.hasAdvanatage = true;

        }
        if (pet1 == "Bird" && pet2 == "Turtle")
        {
            newVal = newValDisadvantage;
            enemyPet.hasDisadvantage = true;


        }
        if (pet1 == "Bird" && pet2 == "Cat")
        {
            newVal = newValDisadvantage;
            enemyPet.hasDisadvantage = true;

        }
        if (pet1 == "Bird" && pet2 == "Rat")
        {
            newVal = newValAdvantage;
            enemyPet.hasAdvanatage = true;

        }
        if (pet1 == "Dog" && pet2 == "Gecko")
        {
            newVal = newValDisadvantage;
            enemyPet.hasDisadvantage = true;

        }
        if (pet1 == "Dog" && pet2 == "Cat")
        {
            newVal = newValAdvantage;
            enemyPet.hasAdvanatage = true;

        }
        if (pet1 == "Cat" && pet2 == "Gecko")
        {
            newVal = newValAdvantage;
            enemyPet.hasAdvanatage = true;

        }
        if (pet1 == "Cat" && pet2 == "Bird")
        {
            newVal = newValAdvantage;
            enemyPet.hasAdvanatage = true;

        }
        if (pet1 == "Cat" && pet2 == "Dog")
        {
            newVal = newValDisadvantage;
            enemyPet.hasDisadvantage = true;

        }
        if (pet1 == "Cat" && pet2 == "Rat")
        {
            newVal = newValAdvantage;
            enemyPet.hasAdvanatage = true;

        }
        if (pet1 == "Rat" && pet2 == "Turtle")
        {
            newVal = newValAdvantage;
            enemyPet.hasAdvanatage = true;

        }
        if (pet1 == "Rat" && pet2 == "Bird")
        {
            newVal = newValDisadvantage;
            enemyPet.hasDisadvantage = true;

        }
        if (pet1 == "Rat" && pet2 == "Cat")
        {
            newVal = newValDisadvantage;
            enemyPet.hasDisadvantage = true;

        }

        return newVal;
    }
}
