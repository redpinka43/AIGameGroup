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
    public EnemyPetChanger enemyPetChanger;
    public GameObject playerParty;
    public GameObject startBattlePanel;
    public GameObject thisPanel;
    // Use this for initialization
    void Start()
    {

        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        player = GameObject.Find("Player").GetComponent<Player>();
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enableVictoryScreen = GameObject.Find("BattleManager").GetComponent<enableVictoryScreen>();
        txt.text = "The enemy " + enemyPet.petName + " has fainted!";
        playerParty = GameObject.Find("Player Pets");

    }

    // Update is called once per frame
    void OnEnable()
    {
        if (enemyPet.currentHealth < 1)
        {
            txt.text = "The enemy " + enemyPet.petName + " has fainted!";
            textFeedBackButton.onClick.AddListener(ShowXP);

        }
    }

    private void ShowXP()
    {
        textFeedBackButton.onClick.RemoveAllListeners();
        txt.text = playerPet.petName + " gains ";
        XPGain();


        if (player.playerPets.Count < 6)
        {


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
                Debug.Log("Catch turned on");
                textFeedBackButton.onClick.AddListener(CatchAttempt);
            }

            Debug.Log("player owns" + playerOwned);
            Debug.Log("owned by enemy" + enemyPet.owned);
        }
        else
        {
            textFeedBackButton.onClick.RemoveAllListeners();
            textFeedBackButton.onClick.AddListener(EndBattle);
        }
         if (enemyPet.owned == false)
        {

            textFeedBackButton.onClick.RemoveAllListeners();
            textFeedBackButton.onClick.AddListener(EndBattle);

        }

        // check if there's more pets to fight, you're in a trainer battle
        if (enemyPet.owned == true)
        {
            // battle either ends or next trainer pet comes out
            if (enemyPetChanger.i < enemyPetChanger.enemyPetParty.transform.childCount)
            {
                enemyPetChanger.petFlag = false;
                enemyPetChanger.i++;
                textFeedBackButton.onClick.AddListener(NextPet);
            }
            else
            {
                textFeedBackButton.onClick.AddListener(VictoryText);
            }
        }

        // battle could end here


    }

    void NextPet()
    {
        txt.text = "The Enemy trainer sends out the next pet!";
        textFeedBackButton.onClick.RemoveAllListeners();
        textFeedBackButton.onClick.AddListener(StartPanel);
        Debug.Log("well at least you got here");

    }

    void VictoryText()
    {
        textFeedBackButton.onClick.RemoveAllListeners();
        txt.text = "Holy moly you won the battle! ";
        textFeedBackButton.onClick.AddListener(EndBattle);
    }
    public void StartPanel()
    {
        textFeedBackButton.onClick.RemoveAllListeners();
        startBattlePanel.SetActive(true);
        thisPanel.SetActive(false);


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
        else
        {
            textFeedBackButton.onClick.AddListener(CatchFail);
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
        else
        {
            textFeedBackButton.onClick.AddListener(CatchFail);
        }
    }

    public void CatchSuccess()
    {
        txt.text = "Success! That was exactly what the wild " + enemyPet.animal + " needed to hear!\n\n<color=blue>The wild " +enemyPet.animal +" has joined your party!</color>";

        ClonePet();

        //end battle on click

    }

    public void ClonePet()
    {
        Pets pet = new Pets();
        pet = Instantiate(enemyPet);
        pet.name = "pet" + player.playerPets.Count;
        pet.currentHealth = enemyPet.health;
        pet.transform.parent = playerParty.transform;

        player.playerPets.Add(pet);
        textFeedBackButton.onClick.RemoveAllListeners();
        textFeedBackButton.onClick.AddListener(EndBattle);

    }
    public void EndBattle()
    {
        enableVictoryScreen.EndBattle();
    }
    public void CatchFail()
    {
        txt.text = "Wow, that was definitely not the right thing to say.The wild " + enemyPet.animal + " looks annoyed..\n\n<color=red>The wild " + enemyPet.animal + " ran off!</color>";

        //end battle on click
        textFeedBackButton.onClick.RemoveAllListeners();
        textFeedBackButton.onClick.AddListener(EndBattle);
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
            txt.text  += "\n\n " +playerPet.petName +" is now level " +playerPet.level +"! ";
        }

        // needs to be shown still
        Debug.Log(playerPet.petName + " gained " + xp + "xp!");
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
