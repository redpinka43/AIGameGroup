using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowPetParty : MonoBehaviour {
    private Player player;
    public Pets playerPet;

    public GameObject thisObject;

    public GameObject petSprite;
    public GameObject petName;
    public GameObject healthBar;

    public Image myImageComponent;
    public Sprite thisImage;
    public Sprite image;

    public int petNum;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player.playerPets.Count < 100)
        {
            thisObject.SetActive(false);
            return;
        }
        playerPet = player.playerPets[petNum];
    }

    private void OnEnable()
    {
       myImageComponent.sprite = playerPet.image;

    }
}
