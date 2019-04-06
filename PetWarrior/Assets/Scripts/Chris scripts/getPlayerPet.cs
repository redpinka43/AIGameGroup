using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPlayerPet : MonoBehaviour
{
    private Player player;
    public Pets playerPet;
    public Pets thisPet;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        thisPet = GameObject.Find("playerPet").GetComponent<Pets>();
        playerPet = player.playerPets[0];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
