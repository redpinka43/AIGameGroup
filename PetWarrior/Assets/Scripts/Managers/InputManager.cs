using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static InputManager instance;
	public float axisSensitivity;

	private bool debouncing = false;
	private List<string> debouncingStrList;

	// Keycode arrays
	KeyCode[] AButton;
	KeyCode[] BButton;
	KeyCode[] CButton;

	KeyCode[] up;
	KeyCode[] down;
	KeyCode[] left;
	KeyCode[] right;

	KeyCode[] exit;
	KeyCode[] fullScreen;


	void Awake () {
		// Singleton pattern
		if (instance == null) {
			instance = this;
			instance.name = "Input Man";
		}
	}


	// Use this for initialization
	void Start () {
		axisSensitivity = 0.5f;
		debouncingStrList = new List<string>();

		// Set keycode arrays
		AButton = new KeyCode[] { KeyCode.Z, KeyCode.J, KeyCode.Return, KeyCode.Space };
		BButton = new KeyCode[] { KeyCode.X, KeyCode.K, KeyCode.RightShift };
		CButton = new KeyCode[] { KeyCode.C, KeyCode.L, KeyCode.Backslash };

		up = new KeyCode[] { KeyCode.UpArrow, KeyCode.W };
		down = new KeyCode[] { KeyCode.DownArrow, KeyCode.S };
		left = new KeyCode[] { KeyCode.LeftArrow, KeyCode.A };
		right = new KeyCode[] { KeyCode.RightArrow, KeyCode.D };

		exit = new KeyCode[] { KeyCode.Escape };
		fullScreen = new KeyCode[] { KeyCode.F4, KeyCode.Tab };

	}


	// Update is called once per frame
	void Update () {

		// Checks if key has gone up if debouncing
		if (debouncing) {
			bool canDisableDebouncing = true;

			List<string> listCopy = new List<string>(debouncingStrList);

			foreach (string keyString in listCopy) {
				if ( getKeyUp(keyString) ) {
					debouncingStrList.Remove(keyString);
				}
				else {
					canDisableDebouncing = false;
				}
			}

			if (canDisableDebouncing) 
				debouncing = false;
		}

	}


	// This is a DEBOUNCED get key down function, meaning if it the key has been pressed down once,
	// but is held down, it will return false until the key has gone up and been pressed again.
	public bool getKeyDown(string keyString) {

		keyString = keyString.ToLower();
		KeyCode[] key = keySwitchStatement( keyString );
	
		if (!debouncing) {

			// Check keyboard inputs here
			for (int i = 0; i < key.Length; i++) {
				if (Input.GetKeyDown(key[i])) {
					// Debug.Log("Key " + keyString + " is pressed DOWN.");
					return true;
				}		
			}

			// Check external controller inputs here
			if ( inputSwitchStatement ( keyString, "down" ) )
				return true;

			if ( getBinaryControllerInput(keyString) ) {
				debouncing = true;
				
				// Add string to debouncing list, if it isn't there already
				if ( !debouncingStrList.Contains(keyString) ) {
					debouncingStrList.Add(keyString);
				}

				return true;
			}
		}
		
		return false;
		
	}

	
	// Gets key down WITHOUT debouncing 
	public bool getKeyDownUndebounced(string keyString) {

		keyString = keyString.ToLower();
		KeyCode[] key = keySwitchStatement( keyString );
	
		// Check keyboard inputs here
		for (int i = 0; i < key.Length; i++) {
			if (Input.GetKeyDown(key[i])) {
				// Debug.Log("Key " + keyString + " is pressed DOWN.");
				return true;
			}
				
		}

		// Check external controller inputs here
		if ( inputSwitchStatement ( keyString, "down" ) )
			return true;

		return getBinaryControllerInput(keyString);
	}



	public bool getKeyUp(string keyString) {
		keyString = keyString.ToLower();
		KeyCode[] key = keySwitchStatement( keyString );
	
		// Check keyboard inputs here
		for (int i = 0; i < key.Length; i++) {
			if (Input.GetKeyUp(key[i])) {
				// Debug.Log("Key " + keyString + " is pressed UP.");
				return true;
			}
		}

		// Check external controller inputs here
		if ( inputSwitchStatement ( keyString, "up" ) )
			return true;

		return getBinaryControllerInput(keyString); 
	}
	
	// Returns a float value from -1 to 1 representing how far the joystick or dpad is tilted.
	public float getAxisRaw(string axis) {
		axis = axis.ToLower();
		switch (axis) {
			case "vertical":
				return Input.GetAxisRaw("Vertical"); 
			case "horizontal":
				return Input.GetAxisRaw("Horizontal");
			default:
				return 0f;
		}
	}

	KeyCode[] keySwitchStatement(string keyString) {
		switch (keyString) {
			case "a":
				return AButton;
			case "b":
				return BButton;
			case "c":
				return CButton;
			case "up":
				return up;
			case "left":
				return left;
			case "down":
				return down;
			case "right":
				return right;
			case "exit":
				return exit;
			case "fullscreen":
				return fullScreen;

			default:
				Debug.Log("ERROR (InputManager.cs): Invalid keyString passed to keySwitchStatement.");
				return new KeyCode[] {};
		}
	}

	bool inputSwitchStatement(string keyString, string direction) {

		if (direction == "down") {
			switch (keyString) {
				case "a":
					if (Input.GetButtonDown("Fire1")) {
						return true;
					}
					return false;
				case "b":
					if (Input.GetButtonDown("Fire2")) {
						return true;
					}
					return false;
				case "c":
					if (Input.GetButtonDown("Fire3")) {
						return true;
					}
					return false;

				case "exit":
					if (Input.GetButtonDown("Jump")) {
						return true;
					}
					return false;
				case "fullscreen":
					if (Input.GetButtonDown("Submit")) {
						return true;
					}
					return false;		

				default:
					return false;
			}
		}

		else if (direction == "up") {
			switch (keyString) {
				case "a":
					if (Input.GetButtonUp("Fire1")) {
						return true;
					}
					return false;
				case "b":
					if (Input.GetButtonUp("Fire2")) {
						return true;
					}
					return false;
				case "c":
					if (Input.GetButtonUp("Fire3")) {
						return true;
					}
					return false;

				case "exit":
					if (Input.GetButtonUp("Jump")) {
						return true;
					}
					return false;
				case "fullscreen":
					if (Input.GetButtonUp("Submit")) {
						return true;
					}
					return false;		

				default:
					return false;
			}
		}

		else
			Debug.Log("ERROR (InputManager.cs): inputSwitchStatement called without valid direction.");
			return false;
	}

	// Checks the controller for if it pressed a direction. Returns true or false, and ignores axis subtleties.
	bool getBinaryControllerInput(string keyString) {
		switch (keyString) {
			case "up":
				if (Input.GetAxisRaw("Vertical") > axisSensitivity)
					return true;
				break;
			case "left":
				if (Input.GetAxisRaw("Horizontal") < (-1f) * axisSensitivity)
					return true;
				break;
			case "down":
				if (Input.GetAxisRaw("Vertical") < (-1f) * axisSensitivity)
					return true;
				break;
			case "right":
				if (Input.GetAxisRaw("Horizontal") > axisSensitivity)
					return true;
				break;
			default:
				return false;
		}

		return false;
	}

}
