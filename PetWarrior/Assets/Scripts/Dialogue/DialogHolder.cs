using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to any object that, when approached and you press SPACE,
// it'll trigger the attached text.

// Currently, it reads the dialogue lines from an array that I fill out in Unity.
// We want to switch this to read from a CSV file. For example,
// let's say we walk up to an NPC, and the conversation goes like this:

// 3 "Hi, how's it going?"
// 4 "Nice day, isn't it?"
// 5 "You've read 3 lines by now." ->End

// For this, you'd just input the line '3' in Unity.
// However, what if there are more complex things happening, like choices, or the
// start line depends on your serialized progress in the game?

// Will we need to write a separate script for that?
// A custom dialogue script?
// Perhaps in the spreadsheet, it could have a column that references a function to call.
// Also, we should make some universal functions, like a basic choice function.

// Ok, so here's our spreadsheet IDs:
// ID, Notes, Dialog Type, Links to, End Dialog, Thumbnail, Animation/Event?, 
// Text type, Text flow, Box type, Speaker, Text

// Hey, what about when the start line depends on the game progress serialization?
// Should we have a custom function for each "if" situation?


public class DialogHolder : MonoBehaviour {

	public string[] dialogueLines;
	public int startNode;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerStay2D(Collider2D other) 
	{
		if(other.gameObject == PlayerManager.instance.playerObject)
		{
			if( InputManager.instance.getKeyUp("A") )
			{
				if(!DialogueManager.instance.dialogueIsRunning)
				{
					Debug.Log("In DialogHolder.cs: calling DialogueManager.instance.ActivateDialogue()");
					DialogueManager.instance.currentNode = DialogueManager.instance.dialogueNodes[startNode];
					DialogueManager.instance.ActivateDialogue();
				}
			}
		}
	}
}
