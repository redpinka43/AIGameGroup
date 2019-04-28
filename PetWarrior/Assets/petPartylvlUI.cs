using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petPartylvlUI : MonoBehaviour
{
    Text txt;
    private Player player;
    public Pets pet;
    public int i;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        pet = player.playerPets[i];
        txt = GetComponentInChildren<Text>();
        txt.text = "LVL: " + pet.level.ToString();
    }
}