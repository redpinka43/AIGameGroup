using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to any object that, when approached and you press SPACE,
// it'll trigger the attached text.

public class DialogHolder : MonoBehaviour {

	private DialogueManager dMan;
	public string[] dialogueLines;
	// public int startLine;

	// Use this for initialization
	void Start () {
		dMan = FindObjectOfType<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerStay2D(Collider2D other) 
	{
		if(other.gameObject.name == "Player")
		{
			if(Input.GetKeyUp(KeyCode.Space))
			{
				Debug.Log("dialogue should happen now.");

				if(!dMan.dialogActive)
				{
					dMan.dialogLines = dialogueLines;
					dMan.currentLine = 0;
					dMan.ShowDialogue();
				}
			}
		}
	}
}
