using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// meaghan
public class petThreeClicked : MonoBehaviour {

	public Button petThree;
    public SwapPets SwapPets;
    private Pets playerPet;
    // Use this for initialization
    void Start()
    {
        petThree.onClick.AddListener(petThree_Click);
        SwapPets = GameObject.Find("BattleManager").GetComponent<SwapPets>();
    }

    public void petThree_Click()
    {
        SwapPets.pressedButton = 3;
		SwapPets.switchPet();

    }
}
