using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckJesus : MonoBehaviour {

    public Collider2D coll;
    private Pets playerPet;

    // Use this for initialization
    void Start()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();

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
    }
}
