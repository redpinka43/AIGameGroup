using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveTwoButtonText : MonoBehaviour
{
    Text txt;
	string moveName = "Sing";
	int ppLeft = 10;
	int ppTotal = 15;
    private Pets pets;
    void Start()
    {
        pets = GameObject.Find("playerPet").GetComponent<Pets>();
        txt = GetComponentInChildren<Text>();
		txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
	}
    public void useMove(string move)
    {
        pets.health += 1;
        Debug.Log(pets.health);
    }
}
