using System.Collections;
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

}
