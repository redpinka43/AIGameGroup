using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryFeedback : MonoBehaviour
{

    public Pets enemyPet;
    public Player player;
    public Pets playerPet;
    public Text txt;
    public Button textFeedBackButton;
    public GameObject catchPanel;
    public Button nice;
    public Button naughty;
    private bool playerOwned;
    public enableVictoryScreen enableVictoryScreen;
    // Use this for initialization
    void Start()
    {
        txt.text = "The enemy " + enemyPet.name + " has fainted!";
        textFeedBackButton.onClick.AddListener(ShowXP);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowXP()
    {
        textFeedBackButton.onClick.RemoveAllListeners();
        txt.text = playerPet.name + " gains ";
        XPGain();



        // check if you own that particular animal yet
        foreach (var pet in player.playerPets)
        {
            if (enemyPet.animal == pet.animal)
            {
               playerOwned = true;
            }
        }

        // if the pet is not owned yet, you attempt to catch it
        if (enemyPet.owned == false && playerOwned == false)
        {
            textFeedBackButton.onClick.AddListener(CatchAttempt);
        }
        
        // check if there's more pets to fight, you're in a trainer battle
        if(enemyPet.owned == true)
        {
            // battle either ends or next trainer pet comes out
        }

        // battle could end here


    }

    public void CatchAttempt()
    {
        txt.text = "Hey! You don't own that pet yet! Try to convince them to join!";
        textFeedBackButton.onClick.AddListener(CatchPanel);


    }

    public void SaySomethingNice()
    {
        // intro
        txt.text = "<i>After careful consideration you decide to say..</i>\n\n";

        // nice prompt
        txt.text += "\"Wow, have you been working out?\"";
        catchPanel.SetActive(false);
        // if successful, catch the pet
        if(PetTempermentCheck("Nice"))
        {
            textFeedBackButton.onClick.AddListener(CatchSuccess);
        }


    }

    public void SaySomethingNaughty()
    {
        // intro
        txt.text = "<i>After careful consideration you decide to say..</i>\n\n";

        //naughty prompt
        txt.text += "\"If Darwin was right about natural selection, how are you still here?\"";
        catchPanel.SetActive(false);

        // if successful, catch the pet
        if (PetTempermentCheck("Naughty"))
        {
            textFeedBackButton.onClick.AddListener(CatchSuccess);
        }
        //else 
    }

    public void CatchSuccess()
    {
        txt.text = "Success! That was exactly what the wild " + enemyPet.animal + " needed to hear!\n\n<color=blue>The wild " +enemyPet.animal +" has joined your party!</color>";

        ClonePet();

        Debug.Log(player.playerPets[1].animal);
        //end battle on click

    }

    public void ClonePet()
    {
        Pets pet = new Pets();

        pet.name = enemyPet.name;
        pet.animal = enemyPet.animal;
        pet.image = enemyPet.image;
        pet.health = enemyPet.health;
        pet.currentHealth = enemyPet.currentHealth;

        pet.maxAttack = enemyPet.maxAttack;
        pet.attack = enemyPet.attack;

        pet.maxDefense = enemyPet.maxDefense;
        pet.defense = enemyPet.defense;

        pet.special = enemyPet.special;
        pet.speed = enemyPet.speed;
        pet.moveNum = enemyPet.moveNum;
        pet.moves = enemyPet.moves;


        player.playerPets.Add(pet);
    }

    public void CatchFail()
    {
        txt.text = "Wow, that was definitely not the right thing to say.The wild " + enemyPet.animal + " looks annoyed..\n\n<color=red>The wild " + enemyPet.animal + " ran off!</color>";

        //end battle on click

    }

    public void CatchPanel()
    {
        textFeedBackButton.onClick.RemoveAllListeners();
        catchPanel.SetActive(true);
        nice.onClick.AddListener(SaySomethingNice);
        naughty.onClick.AddListener(SaySomethingNaughty);

    }

    public int XPGain()
    {
        int xp = enemyPet.level * 5;
        txt.text += xp + " experience points!";
        playerPet.currentXP += 5;
        if (playerPet.currentXP >= playerPet.xpNeeded)
        {
            playerPet.level++;
            StatsGain();
            txt.text  += "\n\n " +playerPet.name +" is now level " +playerPet.level +"! ";
        }

        // needs to be shown still
        Debug.Log(playerPet.name + " gained " + xp + "xp!");
        Debug.Log(playerPet.level);

        return xp;
    }

    public void StatsGain()
    {
        playerPet.maxAttack += 5;
        playerPet.attack += 5;
        playerPet.defense = playerPet.maxDefense += 5;
        playerPet.special += 5;
        playerPet.health = playerPet.currentHealth += 5;
        playerPet.speed += 5;
    }

    
    public bool PetTempermentCheck(string choice)
    {
        bool passOrFail = false;
        switch (choice)
        {
            case ("Nice"):
                if(enemyPet.animal == "Gecko" || enemyPet.animal == "Turtle" ||
                     enemyPet.animal == "Dog" || enemyPet.animal == "Bird")
                {
                    passOrFail = true;
                }
                 break;
            case ("Naughty"):
                if (enemyPet.animal == "Squirrel" || enemyPet.animal == "Cat" ||
                    enemyPet.animal == "Rat")
                {
                    passOrFail = true;
                }
                break;
            default:
                break;
        }
        return passOrFail;
    }
}
