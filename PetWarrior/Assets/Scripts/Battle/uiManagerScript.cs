using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiManagerScript : MonoBehaviour
{
	
	public void StartGame() 
	{
		SceneManager.LoadScene("battleScreen");
	}
	
	public void LoadGame()
	{
		
	}
	
	public void QuitGame()
	{
		Debug.Log("Has quit game");
		Application.Quit();
	}
}
