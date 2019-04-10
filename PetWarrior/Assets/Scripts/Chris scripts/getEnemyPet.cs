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
        RandomizeStatsForPet(thisPet);

        return thisPet;
    }

    public void RandomizeStatsForPet(Pets thisPet)
    {
        int veryLow = RNG(3, 10);
        int low = RNG(11, 19);
        int med = RNG(20, 27);
        int high = RNG(28, 35);
        int veryHigh = RNG(36, 43);

        switch (thisPet.animal)
        {
           
            
            case ("Gecko"):
                thisPet.maxAttack = thisPet.attack = high;
                thisPet.defense = thisPet.maxDefense = low;
                thisPet.special = med;
                thisPet.health = thisPet.currentHealth = low;
                thisPet.speed = high;
                    break;
            case ("Turtle"):
                thisPet.maxAttack = thisPet.attack = med;
                thisPet.defense = thisPet.maxDefense = high;
                thisPet.special = med;
                thisPet.health = high;
                thisPet.speed = low;
    break;
            case ("Squirrel"):
                thisPet.maxAttack = thisPet.attack = high;
                thisPet.defense = thisPet.maxDefense = high;
                thisPet.special = low;
                thisPet.health = thisPet.currentHealth = med;
                thisPet.speed = med;
    break;
            case ("Bird"):
                thisPet.maxAttack = thisPet.attack = low;
                thisPet.defense = thisPet.maxDefense = low;
                thisPet.special = high;
                thisPet.health = thisPet.currentHealth = med;
                thisPet.speed = med;
    break;
            case ("Dog"):
                thisPet.maxAttack = thisPet.attack = med;
                thisPet.defense = thisPet.maxDefense = med;
                thisPet.special = med;
                thisPet.health = thisPet.currentHealth = med;
                thisPet.speed = med;
    break;
            case ("Cat"):
                thisPet.maxAttack = thisPet.attack = veryLow;
                thisPet.defense = thisPet.maxDefense = veryLow;
                thisPet.special = veryHigh;
                thisPet.health = thisPet.currentHealth = med;
                thisPet.speed = med;
    break;
            case ("Rat"):
                thisPet.maxAttack = thisPet.attack = high;
                thisPet.defense = thisPet.maxDefense = low;
                thisPet.special = med;
                thisPet.health = thisPet.currentHealth = low;
                thisPet.speed = high;
    break;

            default:
                break;
        }

    }
int RNG(int min, int max)
{
    int num;
    num = UnityEngine.Random.Range(min, max + 1);
    return num;
}
}
