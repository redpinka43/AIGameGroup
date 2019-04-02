using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveFourButtonText : MonoBehaviour
{
    Text txt;
	string moveName = "Mimic";
	int ppLeft = 20;
	int ppTotal = 30;

    void Start()
    {
        txt = GetComponentInChildren<Text>();
		txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
	}
}
