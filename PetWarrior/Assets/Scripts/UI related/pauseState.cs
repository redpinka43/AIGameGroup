using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pauseState : MonoBehaviour 
{

	public GameObject pausePanel;
	public GameObject firstSelectedButton;
	
	// Update is called once per frame
	void Update () 
	{		
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
		{
			//  activate panel
			pausePanel.SetActive(!pausePanel.activeSelf);
			
			// set first selected button
			EventSystem.current.SetSelectedGameObject(firstSelectedButton);

			// Disable player movement
			if (GameManager.instance.gameState == GameState.OVERWORLD) {

				if (!pausePanel.activeSelf) 
					PlayerManager.instance.playerObject.GetComponent<PlayerController>().enablePlayerMovement();
				else 
					PlayerManager.instance.playerObject.GetComponent<PlayerController>().stopPlayerMovement();

			}	
		}
		
	}
}
