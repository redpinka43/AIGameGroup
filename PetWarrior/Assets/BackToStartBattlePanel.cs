using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToStartBattlePanel : MonoBehaviour {

    public GameObject fightPanel;
    public GameObject itemsPanel;
    public GameObject startBattlePanel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Backspace) || Input.GetKeyUp(KeyCode.JoystickButton1))
        {
            if(itemsPanel.activeSelf)
            {
                itemsPanel.SetActive(false);
                startBattlePanel.SetActive(true);
            }
            if (fightPanel.activeSelf)
            {
                fightPanel.SetActive(false);
                startBattlePanel.SetActive(true);
            }
        }

    }
}
