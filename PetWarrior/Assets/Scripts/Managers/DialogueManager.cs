using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script manages the dialogue box. It takes in a string array from DialogHolder
// and print it until the end of the array is reached.

// We'll also load in all the dialogue lines here and keep them as a list that can 
// always be accessed.
public class DialogueManager : MonoBehaviour {
	
	public static DialogueManager instance = null;

	// Components to be hooked up at start of scene
	public GameObject dialogueNormal_text;			// Dialogue Normal

	public GameObject dialogueChoice2_questionText;	// Dialogue Choice 2
	public GameObject dialogueChoice2_choice1;
	public GameObject dialogueChoice2_choice2;
	public GameObject dialogueChoice2_selector1;
	public GameObject dialogueChoice2_selector2;

	public GameObject dialogueChoice3_questionText;	// Dialogue Choice 3
	public GameObject dialogueChoice3_choice1;
	public GameObject dialogueChoice3_choice2;
	public GameObject dialogueChoice3_choice3;
	public GameObject dialogueChoice3_selector1;
	public GameObject dialogueChoice3_selector2;
	public GameObject dialogueChoice3_selector3;

	public DialogueNode currentNode;
	public List<DialogueNode> dialogueNodes = new List<DialogueNode>();
	public TextAsset txtFile;

	public bool dialogueIsRunning = false;
	public bool dialogueSelectPending = false;


	void Awake () {
		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "Dialogue Man";

			// Assign events
		}

	}


	// Use this for initialization
	void Start () {

		// Convert the .txt file into a list of nodes.
		// First, split the .txt text into lines:
		// var lines = Regex.Split(txtFile.text, "\n|\r|\r\n");
		var lines = Regex.Split(txtFile.text, "\r\n");

		// Starting at row 4, where the dialogue entries start, 
		// convert the entire spreadsheet to a list of dialogue nodes.
		for (int i = 3; i < lines.Length; i++)
		{
			dialogueNodes.Add(newDialogueNode( lines[i] ));
        }


		// For debugging:

		// for (int i = 0; i < dialogueNodes.Count; i++) {
		// 	printNode(dialogueNodes[i]);
		// }

    }

	// Link up components when new scene is loaded. Called by GUIManager.linkUp()
	public void linkUp () {
		// Dialogue Normal
		dialogueNormal_text = GUIManager.instance.dialogueNormal.transform.Find("Text").gameObject;		

		// Dialogue Choice 2
		dialogueChoice2_questionText = GUIManager.instance.dialogueChoice2.transform.Find("Question Text").gameObject;	
		dialogueChoice2_choice1 = GUIManager.instance.dialogueChoice2.transform.Find("Choice 1").gameObject;	
		dialogueChoice2_choice2 = GUIManager.instance.dialogueChoice2.transform.Find("Choice 2").gameObject;	
		dialogueChoice2_selector1 = GUIManager.instance.dialogueChoice2.transform.Find("Selector 1").gameObject;	
		dialogueChoice2_selector2 = GUIManager.instance.dialogueChoice2.transform.Find("Selector 2").gameObject;	

		// Dialogue Choice 3
		dialogueChoice3_questionText = GUIManager.instance.dialogueChoice3.transform.Find("Question Text").gameObject;	
		dialogueChoice3_choice1 = GUIManager.instance.dialogueChoice3.transform.Find("Choice 1").gameObject;
		dialogueChoice3_choice2 = GUIManager.instance.dialogueChoice3.transform.Find("Choice 2").gameObject;
		dialogueChoice3_choice3 = GUIManager.instance.dialogueChoice3.transform.Find("Choice 3").gameObject;
		dialogueChoice3_selector1 = GUIManager.instance.dialogueChoice3.transform.Find("Selector 1").gameObject;
		dialogueChoice3_selector2 = GUIManager.instance.dialogueChoice3.transform.Find("Selector 2").gameObject;
		dialogueChoice3_selector3 = GUIManager.instance.dialogueChoice3.transform.Find("Selector 3").gameObject;
	}


	// Update is called once per frame.
	void Update () {
		// Only update stuff if the user presses Space.using UnityEngine.SceneManagement;
		if ( InputManager.instance.getKeyUp("A") && dialogueIsRunning) {
			continueDialogue( true );
		}
	}


	// The dialogue box workhorse. Changes the displayed dialogue.
	void continueDialogue (bool continueToNextNode) {

		// If we're on a normal dialogue box, end node, and it hasn't been updated yet, call
		// function again with continueToNextNode == false

		if ( continueToNextNode && !currentNode.hasDialogueOptions && currentNode.isEndNode && dialogueNormal_text.GetComponent<Text>().text != currentNode.text) {
			continueDialogue(false);
		}

		// Deactivate dialogue box, if the nextNode is the endNode
		else if (currentNode.isEndNode && continueToNextNode)
		{
			dialogueIsRunning = false;
			
			// Disable all dialogue types
			GUIManager.instance.dialogueNormal.SetActive(false);
			GUIManager.instance.dialogueChoice2.SetActive(false);
			GUIManager.instance.dialogueChoice3.SetActive(false);

			GUIManager.instance.call_OnDialogueEnd();

			// Change scene, if necessary
			if (currentNode.hasNextScene) {
				SceneManager.LoadScene( currentNode.nextScene );
			}

			// Set dialoguebox to empty
			dialogueNormal_text.GetComponent<Text>().text = "";
		}

		// Continue rendering dialogue
		else {
			// Set GUIManager.guiState based on what the current node type is
			if (currentNode.hasDialogueOptions) {
				// See how many options currentNode has 
				switch (currentNode.dialogueOptions.Length) {
					case 2:
						GUIManager.instance.setGUIState(GUIState.DIALOGUE_CHOICE2);
						break;
					case 3:
						GUIManager.instance.setGUIState(GUIState.DIALOGUE_CHOICE3);
						break;
					default:
						Debug.Log("In DialogueManager.cs: current node doesn't have 2 or 3 dialogue options, when it" 
								+ "said there would be dialogueOptions.");
						printNode(currentNode);
						break;
				}
			} 
			else {
				GUIManager.instance.setGUIState(GUIState.DIALOGUE_NORMAL);
			}

			// Check GUI State, and run the type of Dialogue Box that is called for.
			switch (GUIManager.instance.guiState) {
				case GUIState.DIALOGUE_NORMAL:

					dialogueNormal_text.GetComponent<Text>().text = currentNode.text;

					if (!currentNode.isEndNode) {
						currentNode = dialogueNodes[currentNode.nextNodes[0]];
					}

					break;

				case GUIState.DIALOGUE_CHOICE2:

					if (dialogueSelectPending == false) {
						// Set the option component texts
						dialogueChoice2_questionText.GetComponent<Text>().text = currentNode.text;
						dialogueChoice2_choice1.GetComponent<Text>().text = currentNode.dialogueOptions[0];
						dialogueChoice2_choice2.GetComponent<Text>().text = currentNode.dialogueOptions[1];

						// Set inital selector position
						dialogueChoice2_selector1.SetActive(true);
						dialogueChoice2_selector2.SetActive(false);

						// Enable DialogueOptions.cs, and set the intial selected option there
						dialogueSelectPending = true;
						GetComponent<DialogueOptions>().enabled = true;
						GetComponent<DialogueOptions>().selectedOption = 0;
					}

					else {
						// Set next node based off the selected option in DialogueOptions.cs
						if (continueToNextNode)
							currentNode = dialogueNodes[currentNode.nextNodes[ GetComponent<DialogueOptions>().selectedOption ]];
						
						// Disable DialogueOptions.cs
						GetComponent<DialogueOptions>().enabled = false;
						dialogueSelectPending = false;

						continueDialogue( false );
					}
					break;

				case GUIState.DIALOGUE_CHOICE3:
					if (dialogueSelectPending == false) {
						// Set the option component texts
						dialogueChoice3_questionText.GetComponent<Text>().text = currentNode.text;
						dialogueChoice3_choice1.GetComponent<Text>().text = currentNode.dialogueOptions[0];
						dialogueChoice3_choice2.GetComponent<Text>().text = currentNode.dialogueOptions[1];
						dialogueChoice3_choice3.GetComponent<Text>().text = currentNode.dialogueOptions[2];

						// Set inital selector position
						dialogueChoice3_selector1.SetActive(true);
						dialogueChoice3_selector2.SetActive(false);
						dialogueChoice3_selector3.SetActive(false);

						// Enable DialogueOptions.cs, and set the intial selected option there
						dialogueSelectPending = true;
						GetComponent<DialogueOptions>().enabled = true;
						GetComponent<DialogueOptions>().selectedOption = 0;
					}

					else {
						// Set next node based off the selected option in DialogueOptions.cs
						currentNode = dialogueNodes[currentNode.nextNodes[ GetComponent<DialogueOptions>().selectedOption ]];
						
						// Disable DialogueOptions.cs
						GetComponent<DialogueOptions>().enabled = false;
						dialogueSelectPending = false;

						continueDialogue( false );
					}
					break;

				default:
					break;
			}

		}	
	}

	// This isn't called from anywhere. Perhaps delete in future
	public void ShowBox(string dialogue)
	{
		dialogueIsRunning = true;
	}

	// This activates the dialogue box. Called from DialogHolder.cs.
	public void ActivateDialogue()
	{
		dialogueIsRunning = true;

		GUIManager.instance.call_OnDialogueStart();
	}

	// Takes a row from the spreadsheet (as a string), and converts it to a dialogue node.
	DialogueNode newDialogueNode(string line) 
	{
		// Fields:
		// ID (0),	Speaker (1),	Text (2),	Links to (3),	Dialogue Options (4)
		// Notes (5),	Dialog Type (6),	Animation/Event	Thumbnail (7),	
 		// Thumbnail (8),	Text type (9),    Text sound (10),	Box type (11)

		// Split the line into columns:
		var columns = line.Split('\t');

		// Add the line as a new node:
		var id = columns[0];
		var speaker = columns[1];
		var text = columns[2];
		var links = columns[3];
		var dOptions = columns[4];
		var nextScene = columns[6];

		return new DialogueNode(id, speaker, text, links, dOptions, nextScene);
	}




	/* -------- DEBUG -------- */

	// Prints a single node's values
	void printNode(DialogueNode node) {

		string nextNodesString = "";
		string dialogueOptionsString = "";

		int tempInt;

		if (node.nextNodes != null) {
			for (int i = 0; i < node.nextNodes.Length; i++) {
				tempInt = node.nextNodes[i];
				nextNodesString += tempInt.ToString() + " ";
			}
		}

		if (node.dialogueOptions != null) {
			for (int i = 0; i < node.dialogueOptions.Length; i++) {
				dialogueOptionsString += node.dialogueOptions[i] + " ";
			}
		}
		

		Debug.Log("Printing node... \n"
			    + "ID: " + node.id + "\n"
				+ "Speaker: " + node.speaker + "\n" 
				+ "Text: " + node.text + "\n"
				+ "Links to: " + nextNodesString + "\n"
				+ "Dialogue Options: " + dialogueOptionsString + "\n"
				+ "isEndNode: " + node.isEndNode + "\n"
				+ "hasDialogueOptions: " + node.hasDialogueOptions + "\n"
		);
	}






}
