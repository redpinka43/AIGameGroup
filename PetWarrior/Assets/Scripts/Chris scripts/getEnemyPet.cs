using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getEnemyPet : MonoBehaviour {

    private Enemies enemies;
    public Pets enemyPet;
    public Pets thisPet;
    bool petFlag = false;
    // Use this for initialization
    void Start()
    {
        enemies = GameObject.Find("Enemy Pets").GetComponent<Enemies>();
        thisPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        Debug.Log(thisPet.name);

        enemyPet = enemies.enemyPets[Random.Range(0, 7)];
        thisPet = getPet(enemyPet);
        Debug.Log(thisPet.name);

    }

    // Update is called once per frame
    void Update()
    {
        if (petFlag == false)
        {
            enemyPet = enemies.enemyPets[Random.Range(0, 7)];
            thisPet = getPet(enemyPet);
            petFlag = true;
        }

    }

    public void falseflag()
    {
        petFlag = false;
    }

    public Pets getPet(Pets enemyPet)
    {
        thisPet.name = enemyPet.name;
        thisPet.animal = enemyPet.animal;
        thisPet.image = enemyPet.image;
        thisPet.species = enemyPet.species;
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

        return thisPet;
    }
}
