using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// meaghan
public class petPanelState : MonoBehaviour {

	public GameObject petPanel;
	public GameObject firstSelectedButton;
	
	// Update is called once per frame
	void Update () 
	{		
		if (Input.GetKeyDown(KeyCode.P))
		{
			//  activate panel
			petPanel.SetActive(!petPanel.activeSelf);
			
			// set first selected button
			//EventSystem.current.SetSelectedGameObject(firstSelectedButton);

			// Disable player movement
			if (GameManager.instance.gameState == GameState.OVERWORLD) {

				if (!petPanel.activeSelf) 
					PlayerManager.instance.playerObject.GetComponent<PlayerController>().enablePlayerMovement();
				else 
					PlayerManager.instance.playerObject.GetComponent<PlayerController>().stopPlayerMovement();

			}	
		}
		
	}
}
