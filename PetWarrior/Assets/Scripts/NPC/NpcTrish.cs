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

	public static bool readyToBattle = false;
	public bool callMakeTrishReadyToBattle;
	
	// Update is called once per frame
	void Update () {
		
		if (callMakeTrishReadyToBattle) {
			callMakeTrishReadyToBattle = false;
			makeTrishReadyToBattle();
			readyToBattle = true;
		}
	}

	void makeTrishReadyToBattle() {

		readyToBattle = true;
		GetComponent<NpcDialogueZone>().startNode = battleStartNode;
	}
}
