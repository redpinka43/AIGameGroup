using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This is a node which contains all the information for our dialogue line.
// This corresponds to a single row in the spreadsheet.
public class DialogueNode {

	public int id;
	public string speaker;
	public string text;
	public int[] nextNodes;
	public string[] dialogueOptions;
	public string nextScene;

	public bool isEndNode;
	public bool hasDialogueOptions;
	public bool hasNextScene;


    public DialogueNode(string id, string speaker, string text, string nextNodeStr, 
			   			string dialogueOptionsStr, string nextSceneStr) {
		int.TryParse(id, out this.id);
		
		this.speaker = speaker;
        this.text = text;
		
		// Parse the nextNodeStr for multiple links
        // this.nextNode = nextNode;
		if (Equals( nextNodeStr, "End" )) {
			isEndNode = true;
			hasDialogueOptions = false;
		} 
		else {
			this.isEndNode = false;

			if (nextNodeStr.Contains("|")) {
				// Parse the nextNodeStr for multiple links
				string[] tempStrings = nextNodeStr.Split(new char[] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries );
				nextNodes = new int[tempStrings.Length]; 

				for (int i = 0; i < tempStrings.Length; i++) {
					int.TryParse(tempStrings[i], out nextNodes[i]);
				}
			}
			else {
				// Parse for only the one link
				nextNodes = new int[1];
				int.TryParse(nextNodeStr, out nextNodes[0]);
			}
		}

		// Parse dialogueOptionsStr if needed
		if (string.IsNullOrEmpty( dialogueOptionsStr.Replace(" ", string.Empty) )) {
			hasDialogueOptions = false;
		}
		else {
			// Parse dialogueOptionsStr for options
			dialogueOptions = dialogueOptionsStr.Split(new char[] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries );
			hasDialogueOptions = true;
		}

		// Switch to scene
		if (String.IsNullOrEmpty( nextSceneStr.Replace(" ", String.Empty) )) {
			hasNextScene = false;
		}
		else {
			hasNextScene = true;
			nextScene = nextSceneStr;
		}

	}

}
