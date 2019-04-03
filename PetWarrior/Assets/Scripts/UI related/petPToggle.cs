using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// by meaghan
public class petPToggle : MonoBehaviour {

	public GameObject petPanel;
	
	public void showPets()
	{
		petPanel.SetActive(!petPanel.activeSelf);
		
		if (GameManager.instance.gameState == GameState.OVERWORLD)
		{
			if (!petPanel.activeSelf)
				PlayerManager.instance.playerObject.GetComponent<PlayerController>().enablePlayerMovement();
			else
				PlayerManager.instance.playerObject.GetComponent<PlayerController>().stopPlayerMovement();
		}
	}
}
