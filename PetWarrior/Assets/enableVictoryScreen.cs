using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class enableVictoryScreen : MonoBehaviour {

    private Pets enemyPet;
    private Pets playerPet;
    private GameObject panel;
    private GameObject panel2;
    public GameObject panelEnemyFeedBack;
    private GameObject enemySprite;


    // Use this for initialization
    void Start () {
        panel2 = GameObject.Find("startBattlePanel");
        panel2.SetActive(false);
        panel2.SetActive(true);
        panelEnemyFeedBack.SetActive(false);

        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        panel = GameObject.Find("victoryFeedBackPanel");
        
        enemySprite = GameObject.Find("opponentPetSprite");
        panel.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {
        
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
}
