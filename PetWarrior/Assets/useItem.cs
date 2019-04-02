using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useItem : MonoBehaviour {


    private Player player;
    private Pets playerPet;

    private GameObject item;
    private getNextItem getNextItem;
    public string itemName;
    private int itemnumber;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        getNextItem = GameObject.Find("nextItemButton").GetComponent<getNextItem>();
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
    }

    public void Use()
    {
        switch (itemName)
        {
            case "Potion":
                Potion();
                break;
            case "Antidote":
                ;
                break;
            case "Attack Boost":
                break;
            default:
                Debug.Log("No such item");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        itemnumber = getNextItem.i;
        itemName = player.items[itemnumber];
    }

    public void Potion()
    {
        if(playerPet.currentHealth <= (playerPet.health - 10))
        {
            playerPet.currentHealth += 10;
        }
        else
        {
            playerPet.currentHealth = playerPet.health;
        }
    }
}
