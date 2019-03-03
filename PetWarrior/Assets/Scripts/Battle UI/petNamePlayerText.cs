using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petNamePlayerText : MonoBehaviour
{
	Text txt;
    private Pets playerPet;
	
    // Start is called before the first frame update
    void Start()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        txt = GetComponentInChildren<Text>();
		txt.text = playerPet.name;
    }
}
