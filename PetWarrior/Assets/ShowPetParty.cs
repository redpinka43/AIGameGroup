using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowPetParty : MonoBehaviour {
    private Player player;
    public Pets playerPet;

    public GameObject thisObject;

    public GameObject pet1;
    public GameObject pet2;
    public GameObject pet3;
    public GameObject pet4;
    public GameObject pet5;
    public GameObject pet6;

    public int i;

    
    // Use this for initialization
    void Awake()
    {
        pet2.SetActive(false);
        pet3.SetActive(false);
        pet4.SetActive(false);
        pet5.SetActive(false);
        pet6.SetActive(false);

        player = GameObject.Find("Player").GetComponent<Player>();

        if(player.playerPets.Count > 1)
        {
            pet2.SetActive(true);
            
        }
        if (player.playerPets.Count > 2)
        {
            pet3.SetActive(true);

        }
        if (player.playerPets.Count > 3)
        {
            pet4.SetActive(true);

        }
        if (player.playerPets.Count > 4)
        {
            pet5.SetActive(true);

        }
        if (player.playerPets.Count > 5)
        {
            pet6.SetActive(true);

        }

    }
}
