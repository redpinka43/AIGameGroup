using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : MonoBehaviour {

	public enum Gender
	{
		DEFAULT,	// Fall-back state, should never happen
		GIRL,
		BOY
	};

	public static PlayerController instance = null;

	public float moveSpeed;
	private Animator anim;
	private Rigidbody2D myRigidbody;

	private bool playerIsMoving;
	public Vector2 lastMove;
	public Vector2 lastMoveBeforeSceneChange;
	public bool lockedInPlace;
    public bool canMove; // This is the variable to set if you want to disable movement
	public bool enablePlayerDirectionChanging = true;
    public string startPoint;	
	public Gender gender;
	public float viewAngle; // 70 seems to be a good angle.

	void OnEnable () {
		// Assign events
		GUIManager.OnDialogueStart += stopPlayerMovement;
		GUIManager.OnDialogueEnd += enablePlayerMovement;

	}

	void Awake () {
		// Object singleton
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy (gameObject);
			return;
		}
	}

	void OnDisable () {
		// Remove events
		GUIManager.OnDialogueStart -= stopPlayerMovement;
		GUIManager.OnDialogueEnd -= enablePlayerMovement;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
		canMove = true;
        lockedInPlace = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Reset components
		playerIsMoving = false;

        if (!canMove)
            lockedInPlace = true;
        else
            lockedInPlace = false;

        float axisRaw_Horizontal = InputManager.instance.getAxisRaw("Horizontal");
		float axisRaw_Vertical = InputManager.instance.getAxisRaw("Vertical");

		if(lockedInPlace)
		{
			myRigidbody.velocity = Vector2.zero;

			// Set animation to still
			anim.SetBool("PlayerMoving", false);
		}

		// Fixes player direction bug when overworld scene changes
		else if (!enablePlayerDirectionChanging) {
			overworldSceneChangeFix();
		}

		else {  // Player can move 
			// Move horizontal
			if (InputManager.instance.getKeyDownUndebounced("left") || InputManager.instance.getKeyDownUndebounced("right"))
			{
				myRigidbody.velocity = new Vector2(axisRaw_Horizontal * moveSpeed, myRigidbody.velocity.y);
				playerIsMoving = true;
				lastMove = new Vector2(axisRaw_Horizontal, 0f);
			}

			// Move vertical
			if (InputManager.instance.getKeyDownUndebounced("up") || InputManager.instance.getKeyDownUndebounced("down"))
			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, axisRaw_Vertical * moveSpeed);
				playerIsMoving = true;
				lastMove = new Vector2(0f, axisRaw_Vertical );
			}

			// Stay in place horizontal
			if ( !InputManager.instance.getKeyDownUndebounced("left") && !InputManager.instance.getKeyDownUndebounced("right"))
			{
				myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
			}

			// Stay in place vertical
			if ( !InputManager.instance.getKeyDownUndebounced("up") && !InputManager.instance.getKeyDownUndebounced("down") )
			{
				myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
			}

			anim.SetFloat("MoveX", axisRaw_Horizontal);
			anim.SetFloat("MoveY", axisRaw_Vertical);
			anim.SetBool("PlayerMoving", playerIsMoving);
			setFacingDirection(lastMove);
		}		
	}

	public void setFacingDirection (Vector2 direction) {
		
		anim.SetFloat("LastMoveX", direction.x);
		anim.SetFloat("LastMoveY", direction.y);
	}

	// Called whenever GUIManager.OnDialogueStart() event is called
	public void stopPlayerMovement () {
		
		canMove = false;

	}

	// Called whenever GUIManager.OnDialogueEnd() event is called
	public void enablePlayerMovement () {

		canMove = true;

	}

	// If you just loaded a new scene and the startPoint's start direction is different from the direction
	// you were just moving in, then wait for that direction's keys to come up/stop.
	void overworldSceneChangeFix() {
		
		if (lastMoveBeforeSceneChange == new Vector2(0, -1)) { // Facing down
			if (InputManager.instance.getKeyUp("down")) {
				enablePlayerDirectionChanging = true;
			}
			else if (InputManager.instance.getKeyDown("up") 
					|| InputManager.instance.getKeyDown("right") 
					|| InputManager.instance.getKeyDown("left")) {
				enablePlayerDirectionChanging = true;
			}
		}
		else if (lastMoveBeforeSceneChange == new Vector2(0, 1)) { // Facing up
			if (InputManager.instance.getKeyUp("up")) {
				enablePlayerDirectionChanging = true;
			}
			else if (InputManager.instance.getKeyDown("down") 
					|| InputManager.instance.getKeyDown("right") 
					|| InputManager.instance.getKeyDown("left")) {
				enablePlayerDirectionChanging = true;
			}
		}
		else if (lastMoveBeforeSceneChange == new Vector2(-1, 0)) { // Facing left
			if (InputManager.instance.getKeyUp("left")) {
				enablePlayerDirectionChanging = true;
			}
			else if (InputManager.instance.getKeyDown("up") 
					|| InputManager.instance.getKeyDown("right") 
					|| InputManager.instance.getKeyDown("down")) {
				enablePlayerDirectionChanging = true;
			}
		}
		else if (lastMoveBeforeSceneChange == new Vector2(1, 0)) { // Facing right
			if (InputManager.instance.getKeyUp("right")) {
				enablePlayerDirectionChanging = true;
			}
			else if (InputManager.instance.getKeyDown("up") 
					|| InputManager.instance.getKeyDown("down") 
					|| InputManager.instance.getKeyDown("left")) {
				enablePlayerDirectionChanging = true;
			}
		}
		else {
			Debug.Log("ERROR (PlayerController.cs): lastMoveBeforeSceneChange is invalid Vector2 value.");
		}

	}



}
 