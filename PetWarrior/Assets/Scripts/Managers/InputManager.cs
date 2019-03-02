using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static InputManager instance;
	public float axisSensitivity;

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


	public bool getKeyDown(string keyString) {

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

		// Check external controller inputs here. idk if it really works here, but whatever
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
