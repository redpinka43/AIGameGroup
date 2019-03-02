using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game States
public enum GameState 
{
	DEFAULT,	// Fall-back state, should never happen
	INTRO,		// Game intro
	MAIN_MENU,	// Main menu
	OPTIONS,	// Options from the main menu
	OVERWORLD,	// Where you can walk around
	BATTLE,		// Fight screen
	GAME_OVER	// When you die
};


public class GameManager : MonoBehaviour {
	
	public static GameManager instance = null;

	public  GameState gameState { get; private set; }

	// Events
	public delegate void OnStateChangeHandler();
	public event OnStateChangeHandler OnStateChange;

	// Load...
	// Okay to be clear. These manager just maintain the lists of NPCs, and
	// not their info.


	/* Managers */
	// public AudioManager audioMan;
	// public DialogueManager dialogueMan;
	// public EventManager eventMan;
	// public EnvManager envMan;
	// public GUIManager GUIMan;
	// public InputManager inputMan;
	// public PlayerManager playerMan;
	// public NPCManager NPCMan;
	// public BattleManager battleMan;
	// public SaveManager saveMan;
	// public SceneManager sceneMan;

	
	// Prefab managers to be loaded
	public GameObject dialogueManPrefab; 
	public GameObject eventManPrefab;
	public GameObject GUIManPrefab;
	public GameObject inputManPrefab;
	public GameObject playerManPrefab;
	public GameObject sceneManPrefab;
	
	void Awake () {
		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "Game Man";
			DontDestroyOnLoad(gameObject);
		}

		// Instantiate managers 
		if (DialogueManager.instance == null) 	{ Instantiate(dialogueManPrefab, gameObject.transform); }
		if (EventManager.instance == null)  	{ Instantiate(eventManPrefab, gameObject.transform); }
		if (GUIManager.instance == null)  		{ Instantiate(GUIManPrefab, gameObject.transform); }
		if (InputManager.instance == null)  	{ Instantiate(inputManPrefab, gameObject.transform); }
		if (PlayerManager.instance == null)  	{ Instantiate(playerManPrefab, gameObject.transform); }
		if (MySceneManager.instance == null)  	{ Instantiate(sceneManPrefab, gameObject.transform); }
		
		// Assign events 
		MySceneManager.OnSceneChange += checkSceneGameState;

	}

	public void onSceneChangePrep ()
	{
		checkSceneGameState();
	}

	// Called whenever scene is changed. Called in GUIManager.linkUp()
	void checkSceneGameState () {
		GameState scenesGameState = GameObject.Find("Scene Info").GetComponent<SceneInfo>().sceneGameState;

		if (gameState != scenesGameState)
			SetGameState(scenesGameState);
	}

	public void SetGameState(GameState state){
		gameState = state;
		if (OnStateChange != null)
			OnStateChange();
	}


	public void OnApplicationQuit(){
		// Do quitty stuff here
	}

	// Use this for initialization
	void Start () {
		
	}
	
}
