using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This is a node which contains all the information for our dialogue line.
// This corresponds to a single row in the spreadsheet.
public class DialogueNode {

	public string speaker;
	public string text;
	public int link;

    public DialogueNode(string speaker, string text, int link) {
        this.speaker = speaker;
        this.text = text;
        this.link = link;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
