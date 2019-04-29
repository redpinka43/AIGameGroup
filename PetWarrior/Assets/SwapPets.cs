using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// meaghan
public class SwapPets : MonoBehaviour
{
    public GameObject playerPetParty;
    public Pets playerPet;
    private Player player;
    public int pressedButton;
    public GameObject feedbackPanel;
    public Button feedbackPanelButton;
    public Text txt;
    public turnCheck turncheck;
    public GameObject petPanel;
    public GameObject playerPetSprite;
    public GameObject startbattlePanel;
    public GameObject deathFeedBackPanel;
    // Use this for initialization
    void Start()
    {

        player = GameObject.Find("Player").GetComponent<Player>();
        playerPetParty = GameObject.Find("Player Pets");
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();

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
                if (player.playerPets[1].currentHealth > 1)
                    swap(1);
                break;
            case (3):
                if (player.playerPets[2].currentHealth > 1)
                    swap(2);
                break;
            case (4):
                if (player.playerPets[3].currentHealth > 1)
                    swap(3);
                break;
            case (5):
                if (player.playerPets[4].currentHealth > 1)
                    swap(4);
                break;
            case (6):
                if (player.playerPets[5].currentHealth > 1)
                    swap(5);
                break;
            default: break;
        }
    }

    public void swap(int newPetIndex)
    {
        // perform the swap
        Pets temp = player.playerPets[0];


        // temp is our old pet



        player.playerPets[0] = player.playerPets[newPetIndex];



        player.playerPets[newPetIndex] = temp;
        ChangePet(newPetIndex);
        Debug.Log("swapped pets");
        if (playerPetSprite.activeSelf == false)
        {
            playerPetSprite.SetActive(true);
            petPanel.SetActive(false);
            startbattlePanel.SetActive(true);
            deathFeedBackPanel.SetActive(false);
            return;
        }
        petPanel.SetActive(false);
        feedbackPanel.SetActive(true);
        txt.text = "You sent out a new pet! I really hope the game doesn't crash!";
        feedbackPanelButton.onClick.RemoveAllListeners();
        feedbackPanelButton.onClick.AddListener(EnemyTurn);

    }

    public void EnemyTurn()
    {
        feedbackPanelButton.onClick.RemoveAllListeners();
        turncheck.turnState = 1;
        feedbackPanel.SetActive(false);
        turncheck.EnemyTurn();
    }

    public void ChangePet(int newPetIndex)
    {

        // the game object Pet
        var childObject = player.playerPets[0];

        // The actual pet script holding all the variables and stuff
        var pet = childObject.GetComponent<Pets>();

        var temp = new Pets();

        temp = getPet(temp, playerPet);



        // Swap all of the variables into our enemy pet object
        playerPet = getPet(playerPet, pet);

        childObject = player.playerPets[newPetIndex];
        pet = childObject.GetComponent<Pets>();

        pet = getPet(pet, temp);
    }

    public Pets getPet(Pets thisPet, Pets otherPet)
    {
        thisPet.petName = otherPet.petName;
        thisPet.animal = otherPet.animal;
        thisPet.image = otherPet.image;
        thisPet.panelImage = otherPet.panelImage;
        thisPet.health = otherPet.health;
        thisPet.currentHealth = otherPet.currentHealth;

        thisPet.maxAttack = otherPet.maxAttack;
        thisPet.attack = otherPet.attack;

        thisPet.maxDefense = otherPet.maxDefense;
        thisPet.defense = otherPet.defense;

        thisPet.special = otherPet.special;
        thisPet.speed = otherPet.speed;
        thisPet.currentSpeed = otherPet.currentSpeed;
        thisPet.level = otherPet.level;
        thisPet.xpNeeded = otherPet.xpNeeded;

        thisPet.moveNum = otherPet.moveNum;
        thisPet.moves = otherPet.moves;
        thisPet.statusEffects.Clear();

        return thisPet;
    }
}
