using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petNameUI : MonoBehaviour
{
	Text txt;
	string petName = "PURRTY";
	
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<Text>();
		txt.text = petName;
    }
}
