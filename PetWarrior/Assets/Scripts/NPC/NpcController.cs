﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour {

	public enum NpcSkin
	{
		DEFAULT, 	// Fall-back state, should never happen
		BOY,		// 1
		GENTLEMAN,	// 2
		GOTHGIRL,	// 3
		LADY,
		MARY,
		NERD,
		SHOPKEEP
	}
	public NpcSkin npcSkin;
	public string spriteSheetName;
	private string[] spriteSheetNames;

	private Rigidbody2D myRigidbody;
	public float moveSpeed;
	private Animator anim;

	// Movement
	Vector2 currentWalkDistance;
	Vector2 walkStartPosition;
	public bool isWandering = false;

	// Wander
	Vector2 npcStartPosition;
	public float minWaitTime;
	public float maxWaitTime;
	public float wanderRadius;
	public float wanderBoundsRadius;
	float timeWhenWaitingIsDone;


	// Challenging to battle
	public float personalBubbleRadius;  // This determines how far away from you the NPC 
							// stops when they walk up to you. 
							// Your personal space "bubble"
	public bool challengeable = false;
	public int startBattleDialogueNode;
	bool aboutToBattle = false;

	// Debug
	public bool testingMovement = false;
	public Vector2 testVector;


	#region MONOBEHAVIOR

	// Use this for initialization
	void Start () {

		// Initialize based on the npcSkin value
		spriteSheetNames = new string[] {"npc_boy",
									 	 "npc_gentleman",
										 "npc_gothgirl",
										 "npc_lady",
										 "npc_mary",
										 "npc_nerd",
										 "npc_shopkeep" };

		if (npcSkin == NpcSkin.DEFAULT) {
			Debug.Log("ERROR (NpcController.cs): npcSkin hasn't been set to a valid value.");

		}
		else {
			spriteSheetName = spriteSheetNames[(int) npcSkin - 1];
		}

		myRigidbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		npcStartPosition = new Vector2(transform.position.x, transform.position.y);
	
	}

	// Update is called once per frame
	void Update () {

		// Test function
		if (testingMovement) {
			if (walk( testVector )) {
				Debug.Log("Walked " + testVector.x + " in X axis, and " + testVector.y + " in Y axis.");
			}
			
			testingMovement = false;
		}

		// If walking
		if (currentWalkDistance != Vector2.zero) {

			// Check if we're done walking
			if ( Mathf.Abs( transform.position.x - walkStartPosition.x ) >= Mathf.Abs(currentWalkDistance.x)
			  && Mathf.Abs( transform.position.y - walkStartPosition.y ) >= Mathf.Abs(currentWalkDistance.y) ) {

				// Set direction to face
				anim.SetFloat("LastMoveX", currentWalkDistance.normalized.x);
				anim.SetFloat("LastMoveY", currentWalkDistance.normalized.y);
				anim.SetBool("PlayerMoving", false);

				// Stop movement
				currentWalkDistance = Vector2.zero;
				myRigidbody.velocity = Vector2.zero;
				anim.SetFloat("MoveX", 0f);
				anim.SetFloat("MoveY", 0f);
			}
		}

		else if (aboutToBattle) {
			// Start battle dialogue
			DialogueManager.instance.currentNode = DialogueManager.instance.dialogueNodes[startBattleDialogueNode];
			DialogueManager.instance.ActivateDialogue();
			aboutToBattle = false;
		}

		else if (isWandering) {
			wander();
		}

	}

	// This is where the spritesheet is swapped out to fit the type of NPC
	void LateUpdate() {
		
		if (npcSkin != NpcSkin.DEFAULT) {
			var newSprites = Resources.LoadAll<Sprite>("NPCs/" + spriteSheetName);

			foreach (var renderer in GetComponentsInChildren<SpriteRenderer>()) {
				string spriteName = renderer.sprite.name;
				var newSprite = Array.Find(newSprites, item => item.name == spriteName);

				if (newSprite) {
					renderer.sprite = newSprite;
				}
			}
		}
		
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (challengeable) {
			if(other.gameObject == PlayerManager.instance.playerObject) {
				Debug.Log("In the zone");
				approachAndChallengePlayer();
			}
		}
	}
	#endregion

	#region MOVEMENT

	// Walks a distance specified like "new Vector2(0, 200)"
	public bool walk(Vector2 distance) {

		// Check if we still need to finish a current walk
		if (distance == Vector2.zero || currentWalkDistance != Vector2.zero) {
			return false;
		}

		currentWalkDistance = distance;
		walkStartPosition = new Vector2(transform.position.x, transform.position.y);

		// Set velocity
		distance.Normalize();
		myRigidbody.velocity = new Vector2( distance.x * moveSpeed, distance.y * moveSpeed );

		anim.SetFloat("MoveX", distance.x);
		anim.SetFloat("MoveY", distance.y);
		anim.SetBool("PlayerMoving", true);

		return true;
	}

	// Sets the direction the NPC is facing
	public void setNpcAngle(float angle) {

		Vector2 NpcDirection;

		switch((int)angle) {
			case 0:
				NpcDirection = new Vector2(1f, 0f);
				break;
			case 90:
				NpcDirection = new Vector2(0f, 1f);
				break;
			case 180:
				NpcDirection = new Vector2(-1f, 0f);
				break;
			case 270:
				NpcDirection = new Vector2(0f, -1f);
				break;

			default:
				Debug.Log("ERROR (NpcController.cs): angle passed to setNpcAngle not valid.");
				NpcDirection = new Vector2(0f, 0f);
				break;
		}

		anim.SetFloat("LastMoveX", NpcDirection.x);
		anim.SetFloat("LastMoveY", NpcDirection.y);

	}

	// Has the NPC move around randomly in Pokemon-type style
	void wander() {
		
		// public float minWaitTime;
		// public float maxWaitTime;
		// public float wanderRadius;
		// public float wanderBoundsRadius;

		// If still waiting, do nothing 
		if (Time.time < timeWhenWaitingIsDone) {
			return;
		}
		// Else, start a new wander
		else {
			timeWhenWaitingIsDone = Time.time + UnityEngine.Random.Range(minWaitTime, maxWaitTime);

			bool newWanderTargetFound = false;
			while (!newWanderTargetFound) {
				
				// Generate a new wander target position within wanderRadius
				float wanderTargetX = UnityEngine.Random.Range(-wanderRadius, wanderRadius);
				float wanderTargetY = UnityEngine.Random.Range(-wanderRadius, wanderRadius);
				Vector2 newWanderTarget = new Vector2(transform.position.x + wanderTargetX, transform.position.y + wanderTargetY);

				// If it's not in wanderBoundsRadius though, it doesn't count
				if ( (Mathf.Abs(newWanderTarget.x - npcStartPosition.x) < wanderBoundsRadius) 
				  && (Mathf.Abs(newWanderTarget.y - npcStartPosition.y) < wanderBoundsRadius)) {
					// It counts!
					newWanderTargetFound = true;
					walk( new Vector2(transform.position.x - newWanderTarget.x, transform.position.y - newWanderTarget.y) );
				}

			}

		}
	}
	#endregion

	#region CHALLENGE_TO_BATTLE

	// Function that has NPC approach you and start battle
	void approachAndChallengePlayer() {

		Vector2 playerPosition = PlayerManager.instance.playerObject.transform.position;
		Vector2 moveToPlayerVector = new Vector2( playerPosition.x - transform.position.x, playerPosition.y - transform.position.y );

		// Adjust vector so NPC stops within a certain radius of the player
		Vector2 stopShortVector = new Vector2( moveToPlayerVector.normalized.x * personalBubbleRadius, moveToPlayerVector.normalized.y * personalBubbleRadius );
		Vector2 newMoveToPlayerVector = moveToPlayerVector - stopShortVector;
		
		// Stop player's movement
		PlayerController.instance.stopPlayerMovement();
		walk(newMoveToPlayerVector);
		aboutToBattle = true;

	}
	#endregion

	



}
