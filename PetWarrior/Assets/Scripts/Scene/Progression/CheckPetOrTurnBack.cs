using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script checks that the player has a pet before they leave town 1.
// Otherwise, they're told to turn back.

public class CheckPetOrTurnBack : MonoBehaviour {

	public int startNode;
	public float pushDistance;
	
	void OnTriggerStay2D(Collider2D other) 
	{
		GameObject playerObject =  PlayerManager.instance.playerObject;

		if(other.gameObject == PlayerManager.instance.playerObject)
		{

			if (PartyManager.instance.petParty[0] == null) {

				Vector3 endPosition = new Vector3 (playerObject.transform.position.x, playerObject.transform.position.y + pushDistance, playerObject.transform.position.z);
				Vector3 startPosition = new Vector3 (playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z);

				// Push player backwards
				playerObject.transform.position = Vector3.Lerp(startPosition, endPosition, Time.time);
				
				// Talk to them
				if(!DialogueManager.instance.dialogueIsRunning)
				{
					DialogueManager.instance.forceDialogueLoad = true;
					DialogueManager.instance.currentNode = DialogueManager.instance.dialogueNodes[startNode];
					DialogueManager.instance.ActivateDialogue();
				}

			}

		}
	}
}
