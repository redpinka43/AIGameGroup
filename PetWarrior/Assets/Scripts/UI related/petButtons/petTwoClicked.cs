using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// meaghan
public class petTwoClicked : MonoBehaviour {

	public Button petTwo;
    public SwapPets SwapPets;
    private Pets playerPet;
    // Use this for initialization
    void Start()
    {
        petTwo.onClick.AddListener(petTwo_Click);
        SwapPets = GameObject.Find("BattleManager").GetComponent<SwapPets>();
    }

    public void petTwo_Click()
    {
        SwapPets.pressedButton = 2;
		Debug.Log("swap pet 2 with pet 1");
		SwapPets.switchPet();
    }
}
