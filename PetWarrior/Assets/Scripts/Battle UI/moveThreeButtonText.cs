using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveThreeButtonText : MonoBehaviour
{
    Text txt;
	string moveName = "Heal";
	int ppLeft = 3;
	int ppTotal = 5;

    void Start()
    {
        txt = GetComponentInChildren<Text>();
		txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
	}
}
