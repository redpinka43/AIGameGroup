using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public static CameraController instance = null;

	public GameObject followTarget;
	private Vector3 targetPos;
	private Vector3 theoreticalPos;
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

		// "Catch-up" camera
		/*
		targetPos = new Vector3( (float)Math.Round(followTarget.transform.position.x), (float)Math.Round(followTarget.transform.position.y), transform.position.z);
		theoreticalPos = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

		transform.position = new Vector3( (float)Math.Round(theoreticalPos.x), (float)Math.Round(theoreticalPos.y), transform.position.z);
		*/

		transform.position = new Vector3( (float)Math.Round(followTarget.transform.position.x), (float)Math.Round(followTarget.transform.position.y), transform.position.z);

	}
}
