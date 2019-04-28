﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPetChanger : MonoBehaviour
{
    public GameObject enemyPetParty;
    public Pets enemyPet;
    public Pets thisPet;
    bool petFlag = false;
    public int i;
    private void Awake()
    {
        if (GameObject.Find("Trainer Pets") != null)
        {
            Debug.Log("you're fighting a trainer");

            enemyPetParty = GameObject.Find("Trainer Pets");
            enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();

            Transform[] t = enemyPetParty.GetComponentsInChildren<Transform>();

            Transform theParent = t[0];
            Transform firstChild = t[1];
            Pets firstPet = firstChild.GetComponent<Pets>();

            enemyPet = getPet(enemyPet, firstPet);
        }
        else
            Debug.Log("you're not fighting a trainer");

    }

 

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // check if an enemy pet has been chosen, if not get one and randomize it's stats
        if (petFlag == false)
        {

            //get next pet
            //enemyPet = enemies.enemyPets[Random.Range(0, 7)];
            //thisPet = getPet(enemyPet);
            //petFlag = true;
        }

    }
    // Grab the stats of the enemy pet that is chosen(specifically it's moves and sprite)
    public Pets getPet(Pets thisPet, Pets enemyPet)
    {
        thisPet.petName = enemyPet.petName;
        thisPet.animal = enemyPet.animal;
        thisPet.image = enemyPet.image;
        thisPet.health = enemyPet.health;
        thisPet.currentHealth = enemyPet.currentHealth;

        thisPet.maxAttack = enemyPet.maxAttack;
        thisPet.attack = enemyPet.attack;

        thisPet.maxDefense = enemyPet.maxDefense;
        thisPet.defense = enemyPet.defense;

        thisPet.special = enemyPet.special;
        thisPet.speed = enemyPet.speed;
        thisPet.moveNum = enemyPet.moveNum;
        thisPet.moves = enemyPet.moves;
        thisPet.statusEffects.Clear();

        thisPet.moves[0].moveName = "Shell";

        return thisPet;
    }

}