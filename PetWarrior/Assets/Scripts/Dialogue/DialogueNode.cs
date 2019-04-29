using System;
using System.Reflection;
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
	public delegate void functionCallHandler();
	public event functionCallHandler functionCall;

	public bool isEndNode;
	public bool hasDialogueOptions;
	public bool hasNextScene;
	public bool hasFunctionCall;


    public DialogueNode(string id, string speaker, string text, string nextNodeStr, 
			   			string dialogueOptionsStr, string functionCallStr) {
		int.TryParse(id, out this.id);
		
		this.speaker = speaker;
        this.text = addLineBreaks(text, DialogueManager.instance.maxCharsPerLine_dialogueText);
		
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
		if (String.IsNullOrEmpty( functionCallStr.Replace(" ", String.Empty) )) {
			hasNextScene = false;
		}
		else {
			hasNextScene = true;
			nextScene = functionCallStr;
		}


		// See if there's a function call associated with this
		if (String.IsNullOrEmpty( functionCallStr.Replace(" ", String.Empty) )) {
			hasFunctionCall = false;
		}
		else {
			hasFunctionCall = true;
			
			if (hasFunctionCall) {
				parseFunctionCall(functionCallStr);
			}
			

			// functionCall += debugFunction;

			// MethodInfo theMethod = this.GetType().GetMethod(functionCallStr);
			// functionCall = Delegate.CreateDelegate(typeof(void), theMethod);
			// functionCall += theMethod;
		}

	}


	// Adds '\n' every maxCharsPerLine characters.
	string addLineBreaks(string text, int maxCharsPerLine) {

		if (String.IsNullOrEmpty(text)) {
			return "";
		}

		int appendedCharacters = 0;
		string newText = "";

		while (appendedCharacters < text.Length - (text.Length % maxCharsPerLine)) {

			string nextLineChars = findNextLineChars( text.Substring(appendedCharacters), maxCharsPerLine );			
			newText = newText + nextLineChars + "\n";
			appendedCharacters += nextLineChars.Length;

			// Get rid of a single space at the start of new lines
			if (appendedCharacters < text.Length && text[appendedCharacters] == ' ') {
				appendedCharacters++;
			}
		}

		// Append the remainder
		newText = newText + text.Substring(appendedCharacters, text.Length - appendedCharacters);

		return newText;
	}

	// Finds the string that contains the most whole words in the next line
	string findNextLineChars(string text, int maxCharsPerLine) {
		
		// Check for empty strings and maxCharsPerLine errors
		if (String.IsNullOrEmpty(text) || maxCharsPerLine <= 0) {
			return "";
		}

		// Check for 1-character long strings
		if (text.Length == 1) {
			return text;
		}

		// Check if the text is shorter than maxCharsPerLine
		if (text.Length <= maxCharsPerLine) {
			return text;
		}

		// Check if the last character in the line has another character following it
		if (text[maxCharsPerLine - 1] != ' ' && text[maxCharsPerLine] != ' ') {
			
			// Check if the entire line has any spaces in it 
			if ( !text.Substring(0, maxCharsPerLine).Contains(" ")) {
				return text.Substring(0, maxCharsPerLine);
			}

			// Keep stepping back to find the index of the space closest to the end of the line
			int spaceCharIndex = maxCharsPerLine - 1;
			while (text[spaceCharIndex] != ' ') {
				spaceCharIndex--;
			}

			return text.Substring(0, spaceCharIndex);
		}

		// The last character of the line is a space ' ', so return the string before that space
		return text.Substring(0, maxCharsPerLine);
	}


	// Parses the functionCallStr
	void parseFunctionCall(string functionStr) {

		switch (functionStr) {			

			case "debugFunction":
				functionCall += debugFunction;
				break;

			case "battleScreen1":
				functionCall += battleScreen1;
				break;

			case "battleScreen2":
				functionCall += battleScreen1;
				break;

			case "battleScreen_trish":
				functionCall += battleScreen_trish;
				break;

			default:
				break;

		}

	}


	#region FUNCTION_CALLS

	public void callFunctionCall() {
		functionCall();
	}

	void debugFunction() {
		Debug.Log("DialogueNode's debugFunction function call was called.");
	}	

	void battleScreen1() {

		MySceneManager.instance.loadBattleScene();
	}

	void battleScreen2() {

		MySceneManager.instance.loadBattleScene();
	}

	void battleScreen_trish() {

		// Change battleScreen music
		

		MySceneManager.instance.loadBattleScene();
	}
	



	#endregion

}
