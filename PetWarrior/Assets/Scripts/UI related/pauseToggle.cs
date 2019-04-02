using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseToggle : MonoBehaviour 
{
	public GameObject pausePanel;
	
	public void showPause()
	{
		pausePanel.SetActive(false);
		
		if (GameManager.instance.gameState == GameState.OVERWORLD) {

				if (!pausePanel.activeSelf) 
					PlayerManager.instance.playerObject.GetComponent<PlayerController>().enablePlayerMovement();
				else 
					PlayerManager.instance.playerObject.GetComponent<PlayerController>().stopPlayerMovement();

			}	
	}
	
}
