using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

// This script manages the dialogue box. It takes in a string array from DialogHolder
// and print it until the end of the array is reached.

// We'll also load in all the dialogue lines here and keep them as a list that can 
// always be accessed.
public class DialogueManager : MonoBehaviour {

	public GameObject dBox;
	public Text dText;

	public bool dialogActive;

	public string[] dialogLines;
	public int currentLine;
	private PlayerController thePlayer;

	public List<DialogueNode> dialogueNodes = new List<DialogueNode>();

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController>();
    }

	// Update is called once per frame
	void Update () {
		// Print next line in dialogue box, when you press space
		if (dialogActive && Input.GetKeyUp(KeyCode.Space))
		{
			// Deactivate dialogue box, if end of string array is reached.
			if (currentLine >= dialogLines.Length)
			{
				dBox.SetActive(false);
				dialogActive = false;

				currentLine = 0;
				thePlayer.canMove = true;
			}

			else {
				dText.text = dialogLines[currentLine];

				currentLine++;
			// dBox.SetActive(false);
			// dialogActive = false;

			}
		}		
	}

	// This isn't called from anywhere. Perhaps delete in future
	public void ShowBox(string dialogue)
	{
		dialogActive = true;
		dBox.SetActive(true);
		dText.text = dialogue;
	}

	// This activates the dialogue box. Called from DialogHolder.cs
	public void ShowDialogue()
	{
		dialogActive = true;
		dBox.SetActive(true); 
		thePlayer.canMove = false;
	}
}
