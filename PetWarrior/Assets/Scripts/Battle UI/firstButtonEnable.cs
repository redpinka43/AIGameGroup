using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class firstButtonEnable : MonoBehaviour
{
	public GameObject firstSelectedButton;


    public void Start()
    {
       
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void OnEnable()
	{
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }
}
