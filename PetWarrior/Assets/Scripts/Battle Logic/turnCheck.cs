using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnCheck : MonoBehaviour
{
    public int turnState = 0;
    private Pets playerPet;
    private Pets enemyPet;
    public moveOneButtonText moveOneButtonText;
    public moveTwoButtonText moveTwoButtonText;
    public moveThreeButtonText moveThreeButtonText;
    public moveFourButtonText moveFourButtonText;
    public enableVictoryScreen enableVictoryScreen;

    public Button feedBackTextButton;
    public Button enemyFeedBackTextButton;
    public GameObject fightPanel;
    public GameObject feedBackPanel;
    public GameObject enemyFeedBackPanel;
    public GameObject startBattlePanel;


    public int turnNumber;
    public int startStatusEffectTurn;
    public int endStatusEffectTurn;

    private void Start()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();

        feedBackTextButton.onClick.AddListener(feedBackTextButton_Click);
        enemyFeedBackTextButton.onClick.AddListener(enemyFeedBackTextButton_Click);

    }
    public void speedCheck()
    {
        if (playerPet.speed >= enemyPet.speed)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }

    }

    // Update is called once per frame
    public void stateCheck()
    {


    }


    public void PlayerTurn()
    {
        switch(playerPet.moveNum)
        {
            case (0):
                moveOneButtonText.ppLeft--;
                moveNumOne();
                break;
            case (1):
                moveTwoButtonText.useMove();
                moveNumTwo();
                break;
            case (2):
                moveThreeButtonText.useMove();
                moveNumThree();
                break;
            case (3):
                moveFourButtonText.useMove();
                moveNumFour();
                break;
            default:break;
        }

        feedBackPanel.SetActive(true);
        fightPanel.SetActive(false);

        // turn state 0 means this was the first move in the turn
        if (turnState == 0)
        {
            // turn state 1 means one attack has occured in the turn.
            turnState = 1;
            // if this PlayerTurn was the first move then the EnemyTurn is next
           
            
        }else
        {
            // turn the state back to 0, meaning the turn is over.
            Debug.Log('d');
            turnState = 0;
            EndTurn();
        }
    }

    public void EnemyTurn()
    {
        enemyFeedBackPanel.SetActive(true);
    }

    public void moveNumOne()
    {
       // moveOneButtonText.useMove(playerPet.moves[0].moveName);
    }
    public void moveNumTwo()
    {
       // moveOneButtonText.useMove(playerPet.moves[1].moveName);
    }
    public void moveNumThree()
    {
        //moveOneButtonText.useMove(playerPet.moves[2].moveName);
    }
    public void moveNumFour()
    {
       // moveOneButtonText.useMove(playerPet.moves[3].moveName);
    }
    public void enemyFeedBackTextButton_Click()
    {

        enemyFeedBackPanel.SetActive(false);
        enableVictoryScreen.CheckPetLife();
        if(playerPet.currentHealth < 1)
        {
            turnState = 1;
        }
        // turn state 0 means this was the first move in the turn
        if (turnState == 0)
        {
            // turn state 1 means one attack has occured in the turn.
            turnState = 1;

            PlayerTurn();

        }else
        {
            // turn the state back to 0, meaning the turn is over.
            turnState = 0;
            EndTurn();
        }

    }
   public void feedBackTextButton_Click()
    {
        enableVictoryScreen.DeathCheck();
        feedBackPanel.SetActive(false);

        if(turnState == 1)
            enableVictoryScreen.LiveCheck();
    }
    public void EndTurn()
    {
        startBattlePanel.SetActive(true);
    }
}
