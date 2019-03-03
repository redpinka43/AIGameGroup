using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GUI States
public enum GUIState 
{
	DEFAULT,		// Fall-back state, should never happen
	NOTHING,		// No GUI being displayed
	INTRO_MENU,		// Main menu when loading the game from intro
	INTRO_OPTIONS,	// Options when selected from INTRO_MENU
	DIALOGUE_NORMAL,	// In-game dialogue box
	DIALOGUE_CHOICE2,	// In-game dialogue box with 2 choices available
	DIALOGUE_CHOICE3,	// In-game dialogue box with 3 choices available
	OVERWORLD_MENU	// In-game overworld menu
};


public class GUIManager : MonoBehaviour {

	public static GUIManager instance = null;

	public GUIState guiState { get; private set; }
	Stack<GUIState> previousGuiStates;

	// GUI elements 
	public GameObject guiCanvas;
	public GameObject dialogueBox;
	public GameObject dialogueNormal;
	public GameObject dialogueChoice2;
	public GameObject dialogueChoice3;

	// Events
	public delegate void OnDialogueStartHandler();
	public static event OnDialogueStartHandler OnDialogueStart;
	public delegate void OnDialogueEndHandler();
	public static event OnDialogueEndHandler OnDialogueEnd;


	void Awake () {
		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "GUI Man";
		}

		// Assign events
		MySceneManager.OnSceneChange += linkUp;
	}


	// Use this for initialization
	void Start () {
		// Initialize guiState stack
		guiState = GUIState.NOTHING;
		previousGuiStates = new Stack<GUIState>();
		previousGuiStates.Push(GUIState.NOTHING); // This should never be removed from the stack

	}


	// Called whenever new scene is loaded
	void linkUp() {


		// Link up components in scene	
		// Dialogue Box components
		if (GameManager.instance.gameState == GameState.OVERWORLD 
		 || GameManager.instance.gameState == GameState.BATTLE) {
			guiCanvas = GameObject.FindWithTag("GUI Canvas");
			dialogueBox = guiCanvas.transform.Find("Dialogue Box").gameObject;
			dialogueNormal = dialogueBox.transform.Find("Dialogue Normal").gameObject;
			dialogueChoice2 = dialogueBox.transform.Find("Dialogue Choice 2").gameObject;
			dialogueChoice3 = dialogueBox.transform.Find("Dialogue Choice 3").gameObject;

			guiCanvas.GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
			
			DialogueManager.instance.linkUp();
		}
		else 
			guiCanvas = null;
			
		
	}


	// Used, for example, in DialogueManager when deciding to use either Choice2 or Choice3
	public void setGUIState (GUIState newGuiState) {
		guiState = newGuiState;

		// Enable GUI components based on which state it is
		switch (guiState) {
			case GUIState.DIALOGUE_NORMAL:

				dialogueNormal.SetActive(true);
				dialogueChoice2.SetActive(false);
				dialogueChoice3.SetActive(false);
				break;

			case GUIState.DIALOGUE_CHOICE2:

				dialogueNormal.SetActive(false);
				dialogueChoice2.SetActive(true);
				dialogueChoice3.SetActive(false);
				break;

			case GUIState.DIALOGUE_CHOICE3:

				dialogueNormal.SetActive(false);
				dialogueChoice2.SetActive(false);
				dialogueChoice3.SetActive(true);
				break;

			default: 
				break;
		}

	}


	// What you want to use when going from NOTHING to something
	public void loadNewGUIState (GUIState newGuiState) {
		previousGuiStates.Push(guiState);
		guiState = newGuiState;
	}


	// Returns to the previous GUI State
	public void closeGUIState () {

		// Check if stack is empty
		if (previousGuiStates.Count <= 0) {
			Debug.Log("ERROR (GUIManager.cs): previousGuiStates called to Pop() when there were "
					+ "no previous GUIStates.");
		} 

		// If you pop the current state and there's NOTHING underneath it, put another NOTHING on the stack.
		if (previousGuiStates.Count <= 0) {
			previousGuiStates.Push(GUIState.NOTHING);
			Debug.Log("WARNING (GUIManager.cs): previousGuiStates was emptied, which shouldn't normally happen.");
		} else
			guiState = previousGuiStates.Peek();
	}


	public void call_OnDialogueStart () {
		// Enable the Dialogue Box, without any text in it. DialogueManager takes care of enabling the text.
		dialogueBox.SetActive(true);

		Debug.Log("In GUIManager.");

		OnDialogueStart();
	}


	public void call_OnDialogueEnd () {
		// Disable the Dialogue Box
		dialogueBox.SetActive(false);

		OnDialogueEnd();
	}

	

}
