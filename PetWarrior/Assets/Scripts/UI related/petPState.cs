using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//by meaghan
public class petPState : MonoBehaviour {
	
	public GameObject petPanel;
	public GameObject firstSelectedButton;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown("joystick button 6"))
		{
			// activate panel
			petPanel.SetActive(!petPanel.activeSelf);
			
			// set first selected button
			EventSystem.current.SetSelectedGameObject(firstSelectedButton);
			
			// Disable player Movement
			if (GameManager.instance.gameState == GameState.OVERWORLD) 
			{
				if (!petPanel.activeSelf) 
					PlayerManager.instance.playerObject.GetComponent<PlayerController>().enablePlayerMovement();
				else 
					PlayerManager.instance.playerObject.GetComponent<PlayerController>().stopPlayerMovement();
			}	
		}
	}
}
