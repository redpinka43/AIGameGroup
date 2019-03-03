using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveOneButtonText : MonoBehaviour
{
	Text txt;
    public GameObject pet;
    public string move;
	string moveName = "Scratch";
	int ppLeft = 30;
	int ppTotal = 30;
    public Button m_YourFirstButton;

    void Start()
    {

        GameObject thePlayer = GameObject.Find("Gecko");
        Pets pet = thePlayer.GetComponent<Pets>();
        Debug.Log(pet.health);
        // click 
        m_YourFirstButton.onClick.AddListener(TaskOnClick);

        // button text
        txt = GetComponentInChildren<Text>();
		txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
	}

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
    }
}
