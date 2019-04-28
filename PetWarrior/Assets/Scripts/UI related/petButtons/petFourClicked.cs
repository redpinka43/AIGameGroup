using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// meaghan
public class petFourClicked : MonoBehaviour {

	public Button petFour;
    public SwapPets SwapPets;
    private Pets playerPet;
    // Use this for initialization
    void Start()
    {
        petFour.onClick.AddListener(petFour_Click);
        SwapPets = GameObject.Find("BattleManager").GetComponent<SwapPets>();
    }

    public void petFour_Click()
    {
        SwapPets.pressedButton = 4;
    }
}
