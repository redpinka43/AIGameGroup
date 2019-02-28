using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveOneButtonText : MonoBehaviour
{
	Text txt;
	string moveName = "Scratch";
	int ppLeft = 30;
	int ppTotal = 30;

    void Start()
    {
        txt = GetComponentInChildren<Text>();
		txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
	}
}
