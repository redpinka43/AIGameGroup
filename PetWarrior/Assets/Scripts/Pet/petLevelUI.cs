using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// meaghan
public class petLevelUI : MonoBehaviour
{
    public int i;
	Text myText;
	private Player player;
	public Pets playerPet;
	public Text name;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.Find("Player").GetComponent<Player>();
    }
	
	void Update()
	{
		// i indicated the placeholder for pet array
		playerPet = player.playerPets[i];

		myText = GetComponentInChildren<Text>();
		myText.text =  "Level:" + playerPet.level.ToString();
	}
}
