using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useItem : MonoBehaviour {


    private Player player;
    private Pets playerPet;
    private Pets enemyPet;

    private GameObject item;
    private getNextItem getNextItem;
    public string itemName;
    private int itemnumber;
    public turnCheck turnCheck;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
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
            case "Gun":
                Gun();
                break;
            default:
                Debug.Log("No such item");
                break;
        }

        turnCheck.turnState = 1;
    }

    // Update is called once per frame
    void Update()
    {
        itemnumber = getNextItem.i;

        itemName = player.items[itemnumber % player.items.Count];
        Debug.Log(itemName);
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

    public void Gun()
    {
       
            enemyPet.currentHealth = 0;
        
    }
}
