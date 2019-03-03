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
	
	public void battleScreenItems()
	{
		SceneManager.LoadScene("battleScreenItems");
	}
	
	public void battleScreenRun()
	{
		SceneManager.LoadScene("battleScreenRun");
	}

	public void battleScreenPets()
	{
		SceneManager.LoadScene("battleScreenPets");
	}
	
	public void battleScreenFight()
	{
		SceneManager.LoadScene("battleScreenFight");
	}
}
