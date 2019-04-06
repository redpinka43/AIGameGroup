using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getPlayerSprite : MonoBehaviour {

    Image myImageComponent;
    public Sprite thisImage;
    public Sprite image;
    public GameObject enemyPetObject;
    public Pets playerPet;

    // Use this for initialization
    void Start()
    {

        myImageComponent = GetComponent<Image>(); //Our image component is the one attached to this gameObject.

        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        myImageComponent.sprite = playerPet.image;

    }
}
