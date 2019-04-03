using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    private getEnemyPet getenemypet;

    public PlayerController playerController;


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

            panel.SetActive(true);
            enemySprite.SetActive(false);

        }
    }

    public void CheckPetLife()
    {
        if (playerPet.currentHealth < 1)
        {

            panelPetDeath.SetActive(true);
            playerSprite.SetActive(false);

        }
    }

    public void EndBattle()
    {
        SceneManager.LoadScene("town_1");
        getenemypet.falseflag();

    }

    public void StartOver()
    {
        playerPet.currentHealth = playerPet.health;
        playerPet.defense = playerPet.maxDefense;
        playerPet.attack = playerPet.maxAttack;

        SceneManager.LoadScene("startMenu");
    }
}
