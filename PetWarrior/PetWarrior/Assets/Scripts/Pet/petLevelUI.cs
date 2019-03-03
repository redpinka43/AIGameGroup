using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petLevelUI : MonoBehaviour
{
    Text txt;
	int petLevel = 10;
	
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<Text>();
		txt.text = "LVL:" + petLevel;
    }
}
