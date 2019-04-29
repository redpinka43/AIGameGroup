using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckJesus : MonoBehaviour {

    public Collider2D coll;
    private Pets playerPet;
    private Player player;
    // Use this for initialization
    void Start()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        player = GameObject.Find("Player").GetComponent<Player>();

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyDown("space"))


            if (InputManager.instance.getKeyDown("a"))
                HealPets();

        }

    }

    public void HealPets()
    {
        playerPet.currentHealth = playerPet.health;
        playerPet.defense = playerPet.maxDefense;
        playerPet.attack = playerPet.maxAttack;
        playerPet.currentSpeed = playerPet.speed;
        playerPet.statusEffects.Clear();

        foreach (var pet in player.playerPets )
        {
            pet.currentHealth = pet.health;
            pet.defense = pet.maxDefense;
            pet.attack = pet.maxAttack;
            pet.currentSpeed = pet.speed;
            pet.statusEffects.Clear();

            foreach(var move in pet.moves)
            {
                move.ppCurrent = move.ppMax;
            }

        }

    }
}
