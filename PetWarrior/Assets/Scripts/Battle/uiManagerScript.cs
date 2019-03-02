using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
