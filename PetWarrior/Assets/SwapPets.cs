using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// meaghan
public class SwapPets : MonoBehaviour {
    
	private Player player;
	public int pressedButton;

    // Use this for initialization
    void Start () {
		
		player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	public void switchPet()
	{
		// will pass to the swap function which pet index is the new current pet
		switch (pressedButton)
        {
            case (1):
				// player cannot swap their current pet with their current pet, turn
				// should not end
                break;
            case (2):
				Debug.Log("will try to swap");
				swap(1);
                break;
            case (3):
				swap(2);
                break;
            case (4):
				swap(3);
                break;
			case (5):
				swap(4);
                break;
			case (6):
				swap(5);
                break;
            default: break;
        }
	}
	
	public void swap(int newPetIndex)
	{
		// perform the swap
		Pets temp = player.playerPets[0];
		player.playerPets[0] = player.playerPets[newPetIndex];
		player.playerPets[newPetIndex] = temp;
		Debug.Log("swapped pets");
	}
}
