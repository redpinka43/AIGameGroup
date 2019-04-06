using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{

    Text txt;
    public string moveName;
    private Pets enemyPet;
    private Pets playerPet;
    public string feedBackString;
    private moveOneButtonText moveOne;
    public Button enemyFeedBackTextButton;

    public bool callFlag = false;
    private void Awake()
    {

        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();
        
        moveOne = GameObject.Find("moveOneButtonTextObject").GetComponent<moveOneButtonText>();


    }
    void Start()
    {
        txt = GetComponentInChildren<Text>();

    }
    private void OnEnable()
    {
        callFlag = false;
        moveName = enemyPet.moves[UnityEngine.Random.Range(0, 3)];
        Debug.Log(moveName);

    }
    private void Update()
    {



        if (callFlag == false)
        {
            if (moveName == "Nip")
                txt.text = enemyPet.name + " nipped " + playerPet.name + " for " + Nip() + " damage! ";
            if (moveName == "Dance")
                txt.text = enemyPet.name + " did a little dance! Enemy Defense lowered by: " + Dance();
            if (moveName == "Sticky Slap")
            {
                int ssDam = StickySlap();
                if (ssDam != 0)
                {
                    txt.text = enemyPet.name + " dealt " + ssDam + " damage. Oh god, what was on it's hand?";
                }
                else
                {
                    txt.text = enemyPet.name + "'s Sticky Slap missed! Bet that feels bad.";
                }

            }
            if (moveName == "Shed Skin")
            {
                txt.text = enemyPet.name + " feels restored!";
            }
            if (moveName == "Acorn Throw")
            {
                Debug.Log("here");
                AcornThrow();
            }
            if (moveName == "Fury Swipes")
            {
                Debug.Log("here");

                FurySwipes();
            }
        }

        callFlag = true;
    }


    public int Nip()
    {
        int damage = (int)(Math.Ceiling((double)enemyPet.attack / (double)playerPet.defense));
        playerPet.currentHealth -= damage;
        return damage;
    }

    public int Dance()
    {
        int val;
        if (playerPet.defense <= 10)
            val = 1;
        else
        {
            val = (playerPet.defense / 10);
        }
        playerPet.defense -= val;

        return val;
    }

    public int StickySlap()
    {
        int rand = RNG(1, 100);
        int damage;
        if (rand <= 75)
        {
            damage = (enemyPet.attack + 10) / enemyPet.defense;
        }
        else
        {
            damage = 0;

        }
        playerPet.currentHealth -= damage;

        return damage;
    }

    public void ShedSkin()
    {
        if (enemyPet.currentHealth <= (enemyPet.health - 5))
        {
            enemyPet.currentHealth += 5;
        }
        else
        {
            enemyPet.currentHealth = enemyPet.health;
        }

        enemyPet.attack = enemyPet.maxAttack;
        enemyPet.defense = enemyPet.maxDefense;


    }

    public void AcornThrow()
    {
        StartCoroutine(GamePauserAT());
    }
    public IEnumerator GamePauserAT()
    {
        int damage = (int)(Math.Ceiling((double)playerPet.attack / (double)enemyPet.defense));
        int total = 0;
        enemyFeedBackTextButton.interactable = false;


        txt.text = enemyPet.name + " begins throwing acorns!";
      
        // first acorn
        yield return new WaitForSeconds(1.0f);

        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " First acorn dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }

        else
            txt.text = enemyPet.name + " First acorn missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // second acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " Second acorn dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " Second acorn missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // third acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's Third acorn dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " 's Third acorn missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // fourth acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's Fourth acorn dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " 's Fourth acorn missed!";

        // total damage
        yield return new WaitForSeconds(1.0f);
        txt.text = enemyPet.name + " dealt " + total + " total damage!";

        yield return new WaitForSeconds(.5f);
        enemyFeedBackTextButton.interactable = true;

    }


    public void FurySwipes()
    {
        StartCoroutine(GamePauserFS());
    }
    public IEnumerator GamePauserFS()
    {
        int damage = (int)(Math.Ceiling((double)playerPet.attack / (double)enemyPet.defense));
        int total = 0;
        enemyFeedBackTextButton.interactable = false;


        txt.text = enemyPet.name + " begins throwing swiping with it's claws!";

        // first acorn
        yield return new WaitForSeconds(1.0f);

        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's First swipe dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }

        else
            txt.text = enemyPet.name + " 's First swipe missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // second acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's Second swipe dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " 's Second swipe missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // third acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's Third swipe dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " 's Third swipe missed!";

        if (playerPet.currentHealth < 1)
        {
            enemyFeedBackTextButton.interactable = true;
            yield break;
        }

        // fourth acorn
        yield return new WaitForSeconds(1.0f);
        if (RNG(1, 2) == 1)
        {
            txt.text = enemyPet.name + " 's Fourth swipe dealt " + damage + " damage!";
            playerPet.currentHealth -= damage;
            total += damage;
        }
        else
            txt.text = enemyPet.name + " 's Fourth swipe missed!";

        // total damage
        yield return new WaitForSeconds(1.0f);
        txt.text = enemyPet.name + " dealt " + total + " total damage!";

        yield return new WaitForSeconds(.5f);
        enemyFeedBackTextButton.interactable = true;

    }

    int RNG(int min, int max)
    {
        int num;
        num = UnityEngine.Random.Range(min, max + 1);
        return num;
    }

}
