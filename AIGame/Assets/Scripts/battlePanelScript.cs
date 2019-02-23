using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class battlePanelScript : MonoBehaviour
{
	public GameObject panel;
	public GameObject previousPanel;
	public GameObject firstSelectedButton;
    
	public void showPanel()
	{
		panel.SetActive(!panel.activeSelf);
		previousPanel.SetActive(!previousPanel.activeSelf);
		EventSystem.current.SetSelectedGameObject(firstSelectedButton);
	}
}
