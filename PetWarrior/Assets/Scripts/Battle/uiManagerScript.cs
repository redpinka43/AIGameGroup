﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiManagerScript : MonoBehaviour
{
	
	public void StartGame() 
	{
		SceneManager.LoadScene("SampleScene");
	}
	
	public void LoadGame()
	{
		SceneManager.LoadScene("testScene2");
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
