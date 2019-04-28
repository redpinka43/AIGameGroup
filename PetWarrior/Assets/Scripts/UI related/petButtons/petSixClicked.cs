using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// meaghan
public class petSixClicked : MonoBehaviour {

	public Button petSix;
    public SwapPets SwapPets;
    private Pets playerPet;
    // Use this for initialization
    void Start()
    {
        petSix.onClick.AddListener(petSix_Click);
        SwapPets = GameObject.Find("BattleManager").GetComponent<SwapPets>();
    }

    public void petSix_Click()
    {
        SwapPets.pressedButton = 6;
		SwapPets.switchPet();

    }
}
