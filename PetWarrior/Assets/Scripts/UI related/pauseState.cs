using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseState : MonoBehaviour 
{

	public GameObject pausePanel;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			pausePanel.SetActive(!pausePanel.activeSelf);
		}
	}
}
