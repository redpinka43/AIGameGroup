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
        int dam = enemyPet.attack / playerPet.defense;
        

        if (callFlag == false)
        {
            if (moveName == "Nip")
                feedBackString = enemyPet.name + " nipped " + playerPet.name + " for " + dam + " damage! ";
            if (moveName == "Dance")
                feedBackString = enemyPet.name + " did a little dance! Enemy Defense lowered by: " + Dance();
            if (moveName == "Sticky Slap")
            {
                int ssDam = StickySlap();
                if (ssDam != 0)
                {
                    feedBackString = enemyPet.name + " dealt " + ssDam + " damage. Oh god, what was on it's hand?";
                }
                else
                {
                    feedBackString = enemyPet.name + "'s Sticky Slap missed! Bet that feels bad.";
                }

            }
            if (moveName == "Shed Skin")
            {
                feedBackString = enemyPet.name + " feels restored!";
            }
        }

        callFlag = true;
        txt.text = feedBackString;
    }

    public void useMove(string move)
    {
        if (moveName == "Nip" && enemyPet.currentHealth >= 1)
            Nip();

    }

    public int Nip()
    {
        int damage = (enemyPet.attack / playerPet.defense);
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


    int RNG(int min, int max)
    {
        int num;
        num = UnityEngine.Random.Range(min, max + 1);
        return num;
    }

}
