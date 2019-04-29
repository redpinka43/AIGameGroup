using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTrish : MonoBehaviour {

	/*
	Functions of script:
	- Make sure Trish has right dialogue depending on whether
	  underlings have been defeated

	*/
	public int battleStartNode = 132;

	public static GameObject npcTrishObj;

	public static bool readyToBattle = false;
	public static bool callMakeTrishReadyToBattle;
	
	void Start() {

		if (npcTrishObj == null) {

			npcTrishObj = gameObject;
		}
	}


	// Update is called once per frame
	void Update () {
		
		if (callMakeTrishReadyToBattle) {
			callMakeTrishReadyToBattle = false;
			npcTrishObj.GetComponent<NpcTrish>().makeTrishReadyToBattle();
			readyToBattle = true;

			Debug.Log("making Trish ready to battle.");
		}
	}

	void makeTrishReadyToBattle() {

		GetComponent<NpcDialogueZone>().startNode = battleStartNode;
	}
}
