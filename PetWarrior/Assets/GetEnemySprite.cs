using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetEnemySprite : MonoBehaviour {

    Image myImageComponent;
    public Sprite thisImage;
    public Sprite image;
    public GameObject enemyPetObject;
    public Pets enemyPet;

    // Use this for initialization
    void Start () {

        myImageComponent = GetComponent<Image>(); //Our image component is the one attached to this gameObject.

        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        myImageComponent.sprite = enemyPet.image;

    }
	

}
