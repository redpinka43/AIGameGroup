using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petNameEnemyText : MonoBehaviour
{
    Text txt;
    private Pets enemyPet;

    // Start is called before the first frame update
    void Start()
    {
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        txt = GetComponentInChildren<Text>();
    }
	
	void Update()
	{
		txt.text = enemyPet.petName;
	}
}
