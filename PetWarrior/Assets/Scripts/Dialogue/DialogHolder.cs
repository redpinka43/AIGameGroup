using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to any object that, when approached and you press SPACE,
// it'll trigger the attached text.

// Currently, it reads the dialogue lines from an array that I fill out in Unity.
// We want to switch this to read from a CSV file. For example,
// let's say we walk up to an NPC, and the conversation goes like this:

// 3 "Hi, how's it going?"
// 4 "Nice day, isn't it?"
// 5 "You've read 3 lines by now." ->End

// For this, you'd just input the line '3' in Unity.
// However, what if there are more complex things happening, like choices, or the
// start line depends on your serialized progress in the game?

// Will we need to write a separate script for that?
// A custom dialogue script?
// Perhaps in the spreadsheet, it could have a column that references a function to call.
// Also, we should make some universal functions, like a basic choice function.

// Ok, so here's our spreadsheet IDs:
// ID, Notes, Dialog Type, Links to, End Dialog, Thumbnail, Animation/Event?, 
// Text type, Text flow, Box type, Speaker, Text

// Hey, what about when the start line depends on the game progress serialization?
// Should we have a custom function for each "if" situation?


public class DialogHolder : MonoBehaviour {

	public int startNode;
	public Vector2 centerOfZone;
	public bool debugging;

	private float viewConeAngle;

	// Use this for initialization
	void Start () {

		// Calculate center of zone
		float xPos = gameObject.GetComponent<Transform>().position.x;
		float yPos = gameObject.GetComponent<Transform>().position.y;
		float xOffset = gameObject.GetComponent<BoxCollider2D>().offset.x;
		float yOffset = gameObject.GetComponent<BoxCollider2D>().offset.y;

		centerOfZone = new Vector2( xPos + xOffset, yPos + yOffset );

		// Set debugging
		debugging = false;

	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerStay2D(Collider2D other) 
	{
		if(other.gameObject == PlayerManager.instance.playerObject)
		{
			if( InputManager.instance.getKeyDown("A") )
			{
				if (playerFacingObject()) 
				{
					if(!DialogueManager.instance.dialogueIsRunning)
					{
						DialogueManager.instance.currentNode = DialogueManager.instance.dialogueNodes[startNode];
						DialogueManager.instance.ActivateDialogue();
					}
				}
			}
		}
	}

	// Checks if the player is facing the center of the collision box / object
	bool playerFacingObject() {
		
		// Get player object info
		float playerX = PlayerManager.instance.playerObject.GetComponent<Transform>().position.x;
		float playerY = PlayerManager.instance.playerObject.GetComponent<Transform>().position.y;
		float playerAngle = findPlayerAngle();
		viewConeAngle = PlayerManager.instance.playerObject.GetComponent<PlayerController>().viewAngle;

		// This is the x component of the distance from the player 
		// to the center of the dialogue holder
		float playerObjectXOffset = centerOfZone.x - playerX;
		float playerObjectYOffset = centerOfZone.y - playerY;

		// Find inverse tangent of that and adjust for direction
		float objectViewCone = calcObjectViewCone(playerObjectXOffset, playerObjectYOffset, playerAngle);

		if (debugging)
			Debug.Log("playerAngle: " + playerAngle + ", objectViewCone: " + objectViewCone + ", viewConeAngle: " + viewConeAngle);

		// If object is within the viewcone
		if ( Mathf.Abs(objectViewCone) < viewConeAngle ) {
			return true;
		}
		return false;

	}

	// This finds the angle that the player is facing
	float findPlayerAngle() {

		Vector2 playerFacingVector = PlayerManager.instance.playerObject.GetComponent<PlayerController>().lastMove;
		
		if (playerFacingVector == Vector2.up) {
			return 90;
		}
		else if (playerFacingVector == Vector2.left) {
			return 180;
		}
		else if (playerFacingVector == Vector2.down) {
			return 270;
		}
		else if (playerFacingVector == Vector2.right) {
			return 0;
		}

		else {
			Debug.Log("ERROR (DialogHolder.cs): findPlayerAngle returned wrong angle.");
			return -1;
		}

	}

	// Calculates the object view cone angle, aka the angle of the object to the direction the
	// player is facing.
	float calcObjectViewCone(float playerX, float playerY, float playerAngle) {
		
		// For the record, playerX and playerY represent 
		// playerObjectXOffset and playerObjectYOffset
		float objectViewCone = Mathf.Atan2( playerY, playerX ) * Mathf.Rad2Deg;
		objectViewCone = (objectViewCone + 360) % 360;

		if (debugging)
			Debug.Log("before calculation: objectViewCone = " + objectViewCone);

		// Now make adjustments based on the direction the player is facing
		if (playerAngle == 0) {		// Facing right

			// If angle is greater than 180, find its supplement
			if (objectViewCone >= 180) {
				objectViewCone = 360 - objectViewCone;
			}

		}

		else {	// Do some fancy calculations to account for the change in perspective

			if (playerAngle == 90) {	// Facing up
			
				// No adjustment needed
			}

			else if (playerAngle == 180) {		// Facing left

				// Just rotate the code for "Facing up" by 90 degrees clockwise
				objectViewCone = (objectViewCone + 360 - 90) % 360;
			}

			else if (playerAngle == 270) {		// Facing down

				// Just rotate the code for "Facing up" by 180 degrees clockwise
				objectViewCone = (objectViewCone + 360 - 180) % 360;
			}

			else {
				Debug.Log("ERROR (DialogHolder.cs): calcObjectViewCone() not detecting playerAngle correctly.");
			}

			// Quadrant 4
			if (playerX >= 0 && playerY < 0)
				objectViewCone = 180 - objectViewCone;

			// Quadrant 1, 2, 3
			else
				objectViewCone = 90 - objectViewCone;
		}
		
		objectViewCone = Mathf.Abs(objectViewCone);

		// If angle is greater than 180, find its supplement
		if (objectViewCone >= 180) {
			objectViewCone = 360 - objectViewCone;
		}

		// Fix a bug with facing down
		if (playerAngle == 270 && playerX > 0) {
			objectViewCone = 90 - objectViewCone;

		}

		if (debugging)
			Debug.Log("after calculation: objectViewCone = " + objectViewCone);

		return objectViewCone;

	}

}
