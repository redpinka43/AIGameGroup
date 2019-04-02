using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemFeedBackScript : MonoBehaviour
{

    Text txt;
    public string itemName;
    private Player player;
    private Pets playerPet;
    private useItem useItem;

    public bool callFlag = false;

    private void Awake()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        player = GameObject.Find("Player").GetComponent<Player>();
        useItem = GameObject.Find("useItemButton").GetComponent<useItem>();

        itemName = useItem.itemName;


    }

    private void OnEnable()
    {
        callFlag = false;
    }
    void Start()
    {
        txt = GetComponentInChildren<Text>();
    }

    private void Update()
    {

        if (callFlag == false)
        {
            if (itemName == "Potion")
                txt.text = "You healed " + playerPet.name + " for 10 health points! Woopdie doo!";


            callFlag = true;
        }
    }
}

