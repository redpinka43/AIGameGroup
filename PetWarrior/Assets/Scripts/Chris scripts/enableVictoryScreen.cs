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
            XPGain();
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
        // Return to overworld
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

    public void XPGain()
    {
        int xp = enemyPet.level * 5;

        playerPet.currentXP += 5;
        if (playerPet.currentXP >= playerPet.xpNeeded)
        {
            playerPet.level++;
            StatsGain();
        }
        Debug.Log(playerPet.name + " gained " + xp + "xp!");
        Debug.Log(playerPet.level);
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
}
