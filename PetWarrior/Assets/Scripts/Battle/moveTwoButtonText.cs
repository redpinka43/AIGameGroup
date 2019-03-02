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

    void Start()
    {
        txt = GetComponentInChildren<Text>();
		txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
	}
}
