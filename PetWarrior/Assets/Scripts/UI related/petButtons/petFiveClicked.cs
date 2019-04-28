using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// meaghan
public class petFiveClicked : MonoBehaviour {

	public Button petFive;
    public SwapPets SwapPets;
    private Pets playerPet;
    // Use this for initialization
    void Start()
    {
        petFive.onClick.AddListener(petFive_Click);
        SwapPets = GameObject.Find("BattleManager").GetComponent<SwapPets>();
    }

    public void petFive_Click()
    {
        SwapPets.pressedButton = 5;
    }
}
