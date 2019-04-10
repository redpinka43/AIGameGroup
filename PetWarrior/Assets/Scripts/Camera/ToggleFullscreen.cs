using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFullscreen : MonoBehaviour {
	// Use this for initialization

	private int width;
	private int height;

	void Start () {
		Screen.SetResolution(768, 432, false);
		Screen.fullScreenMode = FullScreenMode.Windowed;
	}
	
	// Update is called once per frame
	//void Update () {
	//	if ( InputManager.instance.getKeyDown("fullscreen") )
	//	{
	//		if (!Screen.fullScreen)
	//		{
	//			// Save current resolution in case they want to un-fullscreen 
	//			width = Screen.width;
	//			height = Screen.height;

	//			Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
	//			Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
	//			Cursor.visible = false;
	//		} else {
	//			Screen.SetResolution(width, height, false);
	//			Screen.fullScreenMode = FullScreenMode.Windowed;
	//			Cursor.visible = true;
	//		}
	//	}
	//}
}
