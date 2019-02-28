using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public static CameraController instance = null;

	public GameObject followTarget;
	private Vector3 targetPos;
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		
		// Object singleton
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(transform.gameObject);
		} else {
			Destroy (gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
		targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
	}
}
