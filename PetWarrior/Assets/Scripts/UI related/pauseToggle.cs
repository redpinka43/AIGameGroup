using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseToggle : MonoBehaviour 
{
	public GameObject pausePanel;
	
	public void showPause()
	{
		pausePanel.SetActive(!pausePanel.activeSelf);
	}
	
}
