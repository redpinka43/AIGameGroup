﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
public class moveOneButtonText : MonoBehaviour
{
    string moveName;
    public int ppLeft = 30;
    public int ppTotal = 30;
    private Pets playerPet;
    private Pets enemyPet;
    public string usedMove;
    Text txt;
    public StatusEffects effect;

    private void Awake()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();

    }
    void Start()
    {
        moveName = playerPet.moves[0];
        txt = GetComponentInChildren<Text>();
        txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
    }

    private void Update()
    {
        // avoids division by 0
        if (enemyPet.defense == 0)
            enemyPet.defense = 1;

        txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
    }

    public void useMove(string move)
    {
        
        usedMove = playerPet.moves[playerPet.moveNum];
        if (ppLeft == 0)
        {
            return;
        }

        ppLeft--;



    }

    public int Nip()
    {
        int damage = (int)(Math.Ceiling((double)enemyPet.attack / (double)playerPet.defense));
        damage = AnimalAdvantage(damage);
        Debug.Log("ACTUAL DAMAGE:" + damage);
        return damage;
    }


    public int Dance()
    {
        int val;
        if (enemyPet.defense <= 10)
            val = 1;
        else
        {
            val = (enemyPet.defense / 10);
        }
        return val;
    }

    public int StickySlap()
    {
        int rand = RNG(1, 100);
        int damage;
        if (rand <= 75)
        {
            damage = (playerPet.attack + 10) / playerPet.defense;
        }
        else
        {
            damage = 0;

        }
        damage = AnimalAdvantage(damage);
        enemyPet.currentHealth -= damage;
        return damage;
    }

    public void ShedSkin()
    {
        if (playerPet.currentHealth <= (playerPet.health - 5))
        {
            playerPet.currentHealth += 5;
        }
        else
        {
            playerPet.currentHealth = playerPet.health;
        }

        playerPet.attack = playerPet.maxAttack;
        playerPet.defense = playerPet.maxDefense;


    }

    public int Shell()
    {
        int boost = (int)Math.Ceiling((double)playerPet.defense * .2);
        playerPet.defense += boost;
        return boost;
    }

    public int Growl()
    {
        int val = (int)Math.Ceiling((double)enemyPet.defense * .1);
        enemyPet.defense -= val;
        return val;
    }

    public void Cringe()
    {
        effect = new StatusEffects();
        effect.effectType = "Cringe";
        effect.endAfter = 3;
        enemyPet.statusEffects.Add(effect);
        
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
        string pet1 = playerPet.animal;
        string pet2 = enemyPet.animal;
        
        if(pet1 == "Gecko" && pet2 == "Bird")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValDisadvantage;
            playerPet.hasDisadvantage = true;
            
        }
        if (pet1 == "Gecko" && pet2 == "Dog")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValAdvantage;
            playerPet.hasAdvanatage = true;

        }
        if (pet1 == "Gecko" && pet2 == "Cat")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValDisadvantage;
            playerPet.hasDisadvantage = true;

        }
        if (pet1 == "Turtle" && pet2 == "Bird")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValAdvantage;
            playerPet.hasAdvanatage = true;

        }
        if (pet1 == "Turtle" && pet2 == "Rat")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValDisadvantage;
            playerPet.hasDisadvantage = true;

        }
        if (pet1 == "Bird" && pet2 == "Gecko")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValAdvantage;
            playerPet.hasAdvanatage = true;

        }
        if (pet1 == "Bird" && pet2 == "Turtle")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValDisadvantage;
            playerPet.hasDisadvantage = true;


        }
        if (pet1 == "Bird" && pet2 == "Cat")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValDisadvantage;
            playerPet.hasDisadvantage = true;

        }
        if (pet1 == "Bird" && pet2 == "Rat")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValAdvantage;
            playerPet.hasAdvanatage = true;

        }
        if (pet1 == "Dog" && pet2 == "Gecko")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValDisadvantage;
            playerPet.hasDisadvantage = true;

        }
        if (pet1 == "Dog" && pet2 == "Cat")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValAdvantage;
            playerPet.hasAdvanatage = true;

        }
        if (pet1 == "Cat" && pet2 == "Gecko")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValAdvantage;
            playerPet.hasAdvanatage = true;

        }
        if (pet1 == "Cat" && pet2 == "Bird")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValAdvantage;
            playerPet.hasAdvanatage = true;

        }
        if (pet1 == "Cat" && pet2 == "Dog")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValDisadvantage;
            playerPet.hasDisadvantage = true;

        }
        if (pet1 == "Cat" && pet2 == "Rat")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValAdvantage;
            playerPet.hasAdvanatage = true;

        }
        if (pet1 == "Rat" && pet2 == "Turtle")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValAdvantage;
            playerPet.hasAdvanatage = true;

        }
        if (pet1 == "Rat" && pet2 == "Bird")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValDisadvantage;
            playerPet.hasDisadvantage = true;

        }
        if (pet1 == "Rat" && pet2 == "Cat")
        {
            Debug.Log(pet1 + "is fighting a " + pet2);
            newVal = newValDisadvantage;
            playerPet.hasDisadvantage = true;

        }

        return newVal;
    }
        
}
