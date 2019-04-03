using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class uiManagerScript : MonoBehaviour
{
	
	public void StartGame() 
	{
		SceneManager.LoadScene("playersHouse_basement");
	}
	
	public void LoadGame()
	{
		SceneManager.LoadScene("town_1");
	}
	
	public void QuitGame()
	{
		Debug.Log("Has quit game");
		Application.Quit();
	}
	
	public void StartMenu()
	{
		SceneManager.LoadScene("startMenu");
		
		PlayerManager.instance.playerObject.transform.position = new Vector3 ((float) 122.3, (float) -103.7, (float) 0);
		
		
		//GameObject newButton = GameObject.Find("newGameButton");
		//EventSystem.current.SetSelectedGameObject(null);
		//EventSystem.current.SetSelectedGameObject(newButton);
	}
}
