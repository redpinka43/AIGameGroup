﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Like a normal Dialogue Zone except it adds the feature that it turns the NPC to face the player.
public class NpcDialogueZone : DialogHolder {

	void OnTriggerStay2D(Collider2D other) 
	{
		if(other.gameObject == PlayerManager.instance.playerObject)
		{
			if( InputManager.instance.getKeyDown("A") )
			{
				// Calculate center of zone
				float xPos = gameObject.GetComponent<Transform>().position.x;
				float yPos = gameObject.GetComponent<Transform>().position.y;
				float xOffset = gameObject.GetComponent<BoxCollider2D>().offset.x;
				float yOffset = gameObject.GetComponent<BoxCollider2D>().offset.y;

				centerOfZone = new Vector2( xPos + xOffset, yPos + yOffset );

				if (playerFacingObject()) 
				{
					if(!DialogueManager.instance.dialogueIsRunning)
					{
						NpcController npcController = transform.parent.gameObject.GetComponent<NpcController>();
						
						// Turn NPC to face the player
						float playerAngle = findPlayerAngle();
						npcController.setNpcAngle( (playerAngle + 180) % 360 );

						DialogueManager.instance.currentNode = DialogueManager.instance.dialogueNodes[startNode];
						DialogueManager.instance.ActivateDialogue();

						// Set currentBattlingNpc
						NPCManager.instance.currentBattlingNpc = transform.parent.gameObject.GetComponent<NpcController>().npcId;
						Debug.Log("Set NPCManager's currentBattlingNpc to " + NPCManager.instance.currentBattlingNpc);
						NPCManager.instance.updateDefeatedNPCs( (int) NPCManager.instance.currentBattlingNpc );

						// Stop NPC movement
						npcController.tempStoppedWandering = true;
					}
				}
			}
		}
	}


}
