using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class getEnemyPet : MonoBehaviour
{

    private Enemies enemies;
    public Pets enemyPet;
    public Pets thisPet;
    bool petFlag = false;


    // Use this for initialization
    void Start()
    {
        enemies = GameObject.Find("Enemy Pets").GetComponent<Enemies>();
        thisPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        Debug.Log(thisPet.petName);
      
        enemyPet = enemies.enemyPets[Random.Range(0, 7)];
        
        thisPet = getPet(enemyPet);

        Debug.Log(enemyPet.moves[0].moveName);
    }

    // Update is called once per frame
    void Update()
    {
        // check if an enemy pet has been chosen, if not get one and randomize it's stats
        if (petFlag == false)
        {
            enemyPet = enemies.enemyPets[Random.Range(0, 7)];
            thisPet = getPet(enemyPet);
            petFlag = true;
        }

    }
    // Grab the stats of the enemy pet that is chosen(specifically it's moves and sprite)
    public Pets getPet(Pets enemyPet)
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

        RandomizeStatsForPet(thisPet);

        return thisPet;
    }

    public void RandomizeStatsForPet(Pets thisPet)
    {
        // return stats based on a range of  particular values
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
                thisPet.health =  thisPet.currentHealth = high;
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

    public void falseflag()
    {
        petFlag = false;
    }
}
