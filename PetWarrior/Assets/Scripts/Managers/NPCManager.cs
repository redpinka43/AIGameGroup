﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour {

	// Singleton instance
	public static NPCManager instance = null;
	public bool[] NPCsThatHaveApproachedPlayer;
	public bool[] defeatedNPCs;

	void Awake () {

		// Singleton Pattern
		if (instance == null) {
			instance = this;
			instance.name = "NPC Man";
		}

	}

	void OnEnable() {

		MySceneManager.OnSceneChange += setNPCvariables;
	}

	void Start() {

		NPCsThatHaveApproachedPlayer = new bool[15];
		defeatedNPCs = new bool[15];
	}

	void setNPCvariables() {

		switch(SceneManager.GetActiveScene().name) {
			
			case "town_1":
				for (int i = 0; i < NPCsThatHaveApproachedPlayer.Length; i++) {
					if (NPCsThatHaveApproachedPlayer[i]) {

					}
				}
				break;
			
			default:
				break;

		}
	}

	public void updateDefeatedNPCs(int npcId) {

		defeatedNPCs[npcId] = true;
		
		// Trish check
		if (SceneManager.GetActiveScene().name == "town_1" &&
			defeatedNPCs[(int) NpcController.NpcId.TOWN_2_BOY] &&
			defeatedNPCs[(int) NpcController.NpcId.TOWN_2_NERD]) {
			
			NpcTrish.readyToBattle = true;
		}
	}
}