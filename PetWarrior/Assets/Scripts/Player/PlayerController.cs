using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private bool lockedInPlace;

	public string startPoint;	
	public Gender gender;

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
			DontDestroyOnLoad(transform.gameObject);
		} else {
			Destroy (gameObject);
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

		lockedInPlace = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Reset components
		playerIsMoving = false;
		

		float axisRaw_Horizontal = InputManager.instance.getAxisRaw("Horizontal");
		float axisRaw_Vertical = InputManager.instance.getAxisRaw("Vertical");

		if(lockedInPlace)
		{
			myRigidbody.velocity = Vector2.zero;

			// Set animation to still
			anim.SetBool("PlayerMoving", false);
			return;
		}

		// Move horizontal
		if (InputManager.instance.getKeyDown("left") || InputManager.instance.getKeyDown("right"))
		{
			myRigidbody.velocity = new Vector2(axisRaw_Horizontal * moveSpeed, myRigidbody.velocity.y);
			playerIsMoving = true;
			lastMove = new Vector2(axisRaw_Horizontal, 0f);
		}

		// Move vertical
		if (InputManager.instance.getKeyDown("up") || InputManager.instance.getKeyDown("down"))
		{
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, axisRaw_Vertical * moveSpeed);
			playerIsMoving = true;
			lastMove = new Vector2(0f, axisRaw_Vertical );
		}

		// Stay in place horizontal
		if ( !InputManager.instance.getKeyDown("left") && !InputManager.instance.getKeyDown("right"))
		{
			myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
		}

		// Stay in place vertical
		if ( !InputManager.instance.getKeyDown("up") && !InputManager.instance.getKeyDown("down") )
		{
			myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
		}

		anim.SetFloat("MoveX", axisRaw_Horizontal);
		anim.SetFloat("MoveY", axisRaw_Vertical);
		anim.SetBool("PlayerMoving", playerIsMoving);
		anim.SetFloat("LastMoveX", lastMove.x);
		anim.SetFloat("LastMoveY", lastMove.y);
	}

	// Called whenever GUIManager.OnDialogueStart() event is called
	public void stopPlayerMovement () {
		lockedInPlace = true;
	}

	// Called whenever GUIManager.OnDialogueEnd() event is called
	public void enablePlayerMovement () {
		lockedInPlace = false;
	}
	
}
 