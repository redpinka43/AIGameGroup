using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPetChanger : MonoBehaviour
{
    public GameObject enemyPetParty;
    public Pets enemyPet;
    bool petFlag = false;
    public int i;
    Transform[] t;
    private void Awake()
    {
        if (GameObject.Find("Trainer Pets") != null)
        {
            Debug.Log("you're fighting a trainer");

            enemyPetParty = GameObject.Find("Trainer Pets");
            enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();

            t = enemyPetParty.GetComponentsInChildren<Transform>();

            Transform theParent = t[0];
            Transform firstChild = t[1];
            Pets firstPet = firstChild.GetComponent<Pets>();

            enemyPet = getPet(enemyPet, firstPet);
        }
        else
            Debug.Log("you're not fighting a trainer");

    }

    public void ChangePet(int i)
    {

        // the game object Pet
        var childObject = t[i];

        // The actual pet script holding all the variables and stuff
        var pet = childObject.GetComponent<Pets>();

        // Swap all of the variables into our enemy pet object
        enemyPet = getPet(enemyPet, pet);
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
        thisPet.panelImage = enemyPet.panelImage;
        thisPet.health = enemyPet.health;
        thisPet.currentHealth = enemyPet.currentHealth;

        thisPet.maxAttack = enemyPet.maxAttack;
        thisPet.attack = enemyPet.attack;

        thisPet.maxDefense = enemyPet.maxDefense;
        thisPet.defense = enemyPet.defense;

        thisPet.special = enemyPet.special;
        thisPet.speed = enemyPet.speed;
        thisPet.currentSpeed = enemyPet.currentSpeed;
        thisPet.level = enemyPet.level;
        thisPet.xpNeeded = enemyPet.xpNeeded;

        thisPet.moveNum = enemyPet.moveNum;
        thisPet.moves = enemyPet.moves;
           
        thisPet.statusEffects.Clear();


        return thisPet;
    }

}
