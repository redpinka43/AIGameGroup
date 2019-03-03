using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    // To use this example, attach this script to an empty GameObject.
    // Create three buttons (Create>UI>Button). Next, select your
    // empty GameObject in the Hierarchy and click and drag each of your
    // Buttons from the Hierarchy to the Your First Button, Your Second Button
    // and Your Third Button fields in the Inspector.
    // Click each Button in Play Mode to output their message to the console.
    // Note that click means press down and then release.




    //Make sure to attach these Buttons in the Inspector
    public Button m_YourFirstButton;

    public InputField nameField;

    // Player name variable and property to access
    // it from other scripts.
   public string playerName;
    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        m_YourFirstButton.onClick.AddListener(TaskOnClick);
      
    }

    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");
        SubmitName();
        Debug.Log(playerName);

    }

    public string PlayerName
    {
        get { return playerName; }
        set { Debug.Log("You are not allowed to set the player name like that"); }
    }

    //Use this on a "Submit" button to set the playerName variable.
    public void SubmitName()
    {
        if (string.IsNullOrEmpty(nameField.text) == false)
        {
            playerName = nameField.text;
        }
        
    }
}

