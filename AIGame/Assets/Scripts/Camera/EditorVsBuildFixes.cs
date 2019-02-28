using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

// This script fixes the screen display issues for pixel perfect
// when running from editor vs a build.
public class EditorVsBuildFixes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// If running in editor
		if (Application.isEditor) {
			GetComponent<PixelPerfectCamera>().stretchFill = false;
			GetComponent<ToggleFullscreen>().enabled = false;
		}
		else {
			GetComponent<PixelPerfectCamera>().stretchFill = true;
			GetComponent<ToggleFullscreen>().enabled = true;
		}
	}
}
