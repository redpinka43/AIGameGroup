using System;
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

	public enum FaceDirection
	{
		DOWN,
		UP,
		LEFT,
		RIGHT
	}

	public enum NpcId {
	
		DEFAULT, 	// Do nothing
		TOWN_1_GENTLEMAN,
		ROUTE_1_NERD1,
		ROUTE_1_NERD2,
		ROUTE_1_LADY,
		TOWN_2_NERD,
		TOWN_2_BOY,
		TOWN_2_TRISH
	}

	public NpcSkin npcSkin;
	public FaceDirection faceDirection;
	public NpcId npcId;
	public string spriteSheetName;
	private string[] spriteSheetNames;

	private Rigidbody2D myRigidbody;
	public float moveSpeed;
	private Animator anim;

	// Movement
	Vector2 currentWalkDistance;
	Vector2 walkStartPosition;
	public bool isWandering = false;
	public bool tempStoppedWandering = false;

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
	public bool drawDebugRays = false;


	#region MONOBEHAVIOR

	void OnEnable() {

		GUIManager.OnDialogueEnd += enableWander;
	}

	void OnDisable() {

		GUIManager.OnDialogueEnd -= enableWander;
	}

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

		// Initialize facing direction
		Vector2 NpcDirection;

		switch(faceDirection) {
			case FaceDirection.RIGHT:
				NpcDirection = new Vector2(1f, 0f);
				break;
			case FaceDirection.UP:
				NpcDirection = new Vector2(0f, 1f);
				break;
			case FaceDirection.LEFT:
				NpcDirection = new Vector2(-1f, 0f);
				break;
			case FaceDirection.DOWN:
				NpcDirection = new Vector2(0f, -1f);
				break;

			default:
				NpcDirection = new Vector2(0f, 0f);
				break;
		}

		anim.SetFloat("LastMoveX", NpcDirection.x);
		anim.SetFloat("LastMoveY", NpcDirection.y);

		if (npcId != NpcId.DEFAULT) {
			if (NPCManager.instance.NPCsThatHaveApproachedPlayer[(int) npcId]) {
				challengeable = false;
			}
		}
	
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

			// Set currentBattlingNpc
			NPCManager.instance.currentBattlingNpc = npcId;
			Debug.Log("Set NPCManager's currentBattlingNpc to " + npcId);

			// Start battle dialogue
			DialogueManager.instance.currentNode = DialogueManager.instance.dialogueNodes[startBattleDialogueNode];
			DialogueManager.instance.ActivateDialogue();
			aboutToBattle = false;
			tempStoppedWandering = true;
			challengeable = false;

		}

		else if (isWandering && !tempStoppedWandering) {
			wander();
		}

		if (drawDebugRays) {
			drawRays();
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
					
					// Check if the player is in the way; if they are, then try another vector
					if ( !playerIsInWay(newWanderTarget) ) {

						newWanderTargetFound = true;
						walk( new Vector2(transform.position.x - newWanderTarget.x, transform.position.y - newWanderTarget.y) );
					}
					else
						Debug.Log("Try creating another target");
				}
				else {
					float wanderDistance = wanderRadius;

					// Make a vector out of this distance
					Vector2 centerTarget = new Vector2(npcStartPosition.x - transform.position.x, npcStartPosition.y - transform.position.y);
					newWanderTarget = new Vector2(centerTarget.normalized.x * wanderDistance, centerTarget.normalized.y * wanderDistance);

					if ( playerIsInWay(newWanderTarget) ) {
						// Do nothing
						Debug.Log("NPC's path to center blocked by player. Standing still.");
						newWanderTargetFound = true;
					}

					else 
					{
						Debug.Log("Sending NPC back towards center");
						walk(newWanderTarget);
						newWanderTargetFound = true;
					}

				}

			}

		}
	}

	// Checks if the given wanderTarget would end up bumping into the player character
	bool playerIsInWay(Vector2 wanderTarget) {

		return false;
		
		/*
		Vector2 playerPosition = PlayerManager.instance.playerObject.transform.position;
		bool xCollides = false;
		bool yCollides = false;

		// Check if x collides
		if ((transform.position.x - wanderRadius > playerPosition.x - personalBubbleRadius && 
			 transform.position.x - wanderRadius < playerPosition.x + personalBubbleRadius) ||
			(transform.position.x + wanderRadius > playerPosition.x - personalBubbleRadius && 
			 transform.position.x + wanderRadius < playerPosition.x + personalBubbleRadius)) {

				xCollides = true;
		}

		// Check if y collides
		if ((transform.position.y - wanderRadius > playerPosition.y - personalBubbleRadius && 
			 transform.position.y - wanderRadius < playerPosition.y + personalBubbleRadius) ||
			(transform.position.y + wanderRadius > playerPosition.y - personalBubbleRadius && 
			 transform.position.y + wanderRadius < playerPosition.y + personalBubbleRadius)) {

				yCollides = true;
		}

		if (xCollides && yCollides) {
			Debug.Log("playerIsInWay returns true");
			return true;
		}

		else {
			Debug.Log("playerIsInWay returns false");
			return false;
		}
		*/
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

		// Save that this NPC has approached the player before
		NPCManager.instance.NPCsThatHaveApproachedPlayer[(int) npcId] = true;

	}
	#endregion

	void drawRays() {
		
		// NPC's start position vector
		Vector3 startPosition = new Vector3(npcStartPosition.x, npcStartPosition.y, transform.position.z);

		/*  Wander radius */
		// Left ray
		Vector3 start = new Vector3(transform.position.x - wanderRadius, transform.position.y - wanderRadius, transform.position.z);
		Vector3 direction = new Vector3(0f, wanderRadius * 2, 0f);
		Debug.DrawRay(start, direction, Color.green);

		// Bottom ray
		direction = new Vector3(wanderRadius * 2, 0f, 0f);
		Debug.DrawRay(start, direction, Color.green);

		// Right ray
		start = new Vector3(transform.position.x + wanderRadius, transform.position.y + wanderRadius, transform.position.z);
		direction = new Vector3(0f, wanderRadius * -2, 0f);
		Debug.DrawRay(start, direction, Color.green);

		// Top ray
		direction = new Vector3(wanderRadius * -2, 0f, 0f);
		Debug.DrawRay(start, direction, Color.green);


		/* The larger wander boundaries */
		// Left ray
		start = new Vector3(startPosition.x - wanderBoundsRadius, startPosition.y - wanderBoundsRadius, transform.position.z);
		direction = new Vector3(0f, wanderBoundsRadius * 2, 0f);
		Debug.DrawRay(start, direction, Color.green);

		// Bottom ray
		direction = new Vector3(wanderBoundsRadius * 2, 0f, 0f);
		Debug.DrawRay(start, direction, Color.green);

		// Right ray
		start = new Vector3(startPosition.x + wanderBoundsRadius, startPosition.y + wanderBoundsRadius, transform.position.z);
		direction = new Vector3(0f, wanderBoundsRadius * -2, 0f);
		Debug.DrawRay(start, direction, Color.green);

		// Top ray
		direction = new Vector3(wanderBoundsRadius * -2, 0f, 0f);
		Debug.DrawRay(start, direction, Color.green);

		// Player's personal bubble?
	}

	// Enable wander upon dialogue box being disabled
	void enableWander() {
		tempStoppedWandering = false;
	}

}
