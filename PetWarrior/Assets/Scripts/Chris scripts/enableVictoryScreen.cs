using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enableVictoryScreen : MonoBehaviour
{

    private Pets enemyPet;
    private Pets playerPet;
    private GameObject panel;
    private GameObject panel2;
    public GameObject panelEnemyFeedBack;
    public GameObject panelPetDeath;
    private GameObject enemySprite;
    private GameObject playerSprite;
    private Transform playerTransform;
    private Player player;
    public Button petDeathButton;
    public Text petDeathText;
    private getEnemyPet getenemypet;
    public GameObject petPanel;
    private bool isAPetStillAlive;
    public Button backButton;
    public PlayerController playerController;
    public GameObject startBattlePanel;
    public getEnemyPet getpet;
    // Use this for initialization
    void Start()
    {
        panel2 = GameObject.Find("startBattlePanel");
        playerTransform =  GameObject.Find("Player").GetComponent<Transform>();
        getenemypet = GameObject.Find("enemyPet").GetComponent<getEnemyPet>();
        player = GameObject.Find("Player").GetComponent<Player>();
        panel2.SetActive(false);
        panel2.SetActive(true);
        panelEnemyFeedBack.SetActive(false);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.canMove = false;
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        panel = GameObject.Find("victoryFeedBackPanel");
        enemySprite = GameObject.Find("opponentPetSprite");
        playerSprite = GameObject.Find("playerPetSprite");
        panelPetDeath.SetActive(false);

        getpet = GameObject.Find("enemyPet").GetComponent<getEnemyPet>();
        panel.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LiveCheck()
    {
        if (enemyPet.currentHealth >= 1)
        {

            panelEnemyFeedBack.SetActive(true);


        }
    }
    public void DeathCheck()
    {
        if (enemyPet.currentHealth < 1)
        {
        
            // You killed the pet, it goes to the victoryFeedBackPanel and display the necessary text
            panel.SetActive(true);
        }
    }

    public void CheckPetLife()
    {
        if (playerPet.currentHealth < 1)
        {
            panelPetDeath.SetActive(true);
            startBattlePanel.SetActive(false);

            petDeathText.text = "Oh no! Your pet fainted!";

            foreach(var pet in player.playerPets)
            {
                if (pet.petName != playerPet.petName)
                    {
                    if (pet.currentHealth > 1)
                    {
                        //a pet still lives
                        isAPetStillAlive = true;
                    }
                }
            }
            if(isAPetStillAlive)
                {
                petDeathText.text += " Choose the next pet to battle.";


                // choose the next pet
                petDeathButton.onClick.AddListener(ChooseNextPet);
            }
            else
            {
                petDeathText.text += " That was your last pet! I can't believe you actually lost!";
                petDeathButton.onClick.AddListener(StartOver);
            }
            playerSprite.SetActive(false);

        }
    }

    public void ChooseNextPet()
    {
        //pet panel appears
        petPanel.SetActive(true);
        //back button disabled
        backButton.onClick.RemoveAllListeners();
        //disable any other panels
    }
    public void NormalizePets()
    {
       
        playerPet.defense = playerPet.maxDefense;
        playerPet.attack = playerPet.maxAttack;
        playerPet.currentSpeed = playerPet.speed;
        playerPet.statusEffects.Clear();

        foreach (var pet in player.playerPets)
        {
            
            pet.defense = pet.maxDefense;
            pet.attack = pet.maxAttack;
            pet.currentSpeed = pet.speed;
            pet.statusEffects.Clear();

        }

    }
    public void EndBattle()
    {
        NormalizePets();
        getpet.petFlag = false;
        // Return to overworld
        Debug.Log("In EndBattle(), loading scene " + MySceneManager.instance.lastOverworldScene);
        PlayerController.instance.GetComponent<Animator>().SetFloat("MoveX", MySceneManager.instance.lastFacingDirection.x);
        PlayerController.instance.GetComponent<Animator>().SetFloat("MoveY", MySceneManager.instance.lastFacingDirection.y);
        
        // Update defeated NPCs
        NPCManager.instance.updateDefeatedNPCs((int) NPCManager.instance.currentBattlingNpc);

        SceneManager.LoadScene(MySceneManager.instance.lastOverworldScene);
 
        // SceneManager.LoadScene("startMenu");
        // float newX = 122.3F;
        // float newY = -103.7F;
        // player.transform.position = new Vector2(newX, newY);
        getenemypet.falseflag();

    }

    public void StartOver()
    {
        playerPet.currentHealth = playerPet.health;
        playerPet.defense = playerPet.maxDefense;
        playerPet.attack = playerPet.maxAttack;
        SceneManager.LoadScene("startMenu");


        float newX  = 122.3F;
        float newY  = -103.7F;
        player.transform.position = new Vector2(newX, newY);
        getenemypet.falseflag();
    }

}
