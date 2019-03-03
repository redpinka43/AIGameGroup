
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// This is a template for options you can pick, because choices like this will
// probably come up quite often.

// Here's how it will work overall:
// This script is deactivated at first.

// If you, in DialogueManager, you encounter a dialogueNode which has Dialogue Options,
// then it will activate the DialogueOptions.cs script. It will pass the dialogue
// node to this script. (Can you have this script run without it being enabled first?)

// This script will then be enabled, and the called function will modify all the
// rectangles to fit the given options. Then OnGUI() will constantly be drawing 
// out the components as they should be seen. 

// In Update(), we'll be monitoring for the user's input, namely the left and right
// arrow keys, Z, and X. X will skip forward in text printing process, and Z 
// picks the choice you've selected.

// Depending on which choice you've picked, it returns the number node you're supposed
// to progress to, and this is the return value of the function you called in 
// DialogueManager.cs. DialogueManager then disables this script again, and progresses
// to the next script. 


public class DialogueOptions : MonoBehaviour {

	public int selectedOption;	// Set and reset in DialogueManager.cs

	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {
		// Handle all button presses in here
		if (GUIManager.instance.guiState == GUIState.DIALOGUE_CHOICE2)
			handleButtonPressChoice2();

		else if (GUIManager.instance.guiState == GUIState.DIALOGUE_CHOICE3) 
			handleButtonPressChoice3();

		else
			// Disable this script until more dialogue options are encountered
			this.enabled = false;
	}

	private void handleButtonPressChoice2() {
		// If button is being pressed left, move selector if possible
		if ( InputManager.instance.getKeyUp("left") ) {
			switch (selectedOption) {
				case 0:
					// Do nothing
					break;
				case 1:
					// Make selected option 'option 0'.
					selectedOption = 0;
					DialogueManager.instance.dialogueChoice2_selector1.SetActive(true);
					DialogueManager.instance.dialogueChoice2_selector2.SetActive(false);
					break;
				default:
					Debug.Log("ERROR (DialogueOptions.cs): selectedOption is invalid (not 0 or 1).");
					break;
			}
		}

		// If button is being pressed right, move selector if possible
		if ( InputManager.instance.getKeyUp("right") ) {
			switch (selectedOption) {
				case 0:
					// Make selected option 'option 0'.
					selectedOption = 1;
					DialogueManager.instance.dialogueChoice2_selector1.SetActive(false);
					DialogueManager.instance.dialogueChoice2_selector2.SetActive(true);
					break;
				case 1:
					// Do nothing
					break;
				default:
					Debug.Log("ERROR (DialogueOptions.cs): selectedOption is invalid (not 0 or 1).");
					break;
			}
		}
	}

	private void handleButtonPressChoice3() {

	}

}
