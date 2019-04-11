using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

	private PlayerController thePlayer;
	private CameraController theCamera;

	public Vector2 startDirection;

	private string pointName;


	// Use this for initialization

	void Start () {
		pointName = this.name;

		thePlayer = PlayerController.instance;

		if (thePlayer.startPoint == pointName) {
			thePlayer.transform.position = new Vector3(transform.position.x, transform.position.y, thePlayer.transform.position.z);

			thePlayer.lastMoveBeforeSceneChange = thePlayer.lastMove;
			thePlayer.setFacingDirection(startDirection);

			theCamera = CameraController.instance;
			theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
		}

		// Begin FadeSceneTransition's 'Fade' function now, if necessary.
		if (GUIManager.instance.fadeState == GUIManager.FadeState.FADING_OUT ) {

			// Set player variables
			PlayerController.instance.canMove = false;

			// Begin fade process in FadeSceneTransition
			GUIManager.instance.fadeToBlackObj.GetComponent<FadeSceneTransition>().beginFade();
		} 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
