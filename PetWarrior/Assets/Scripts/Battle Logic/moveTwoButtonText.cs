using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class moveTwoButtonText : MonoBehaviour
{

    string moveName;
    public int ppLeft;
    public int ppTotal;
    private Pets playerPet;
    private Pets enemyPet;
    Text txt;
    public Button moveTwoButton;


    private void Awake()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
        enemyPet = GameObject.Find("enemyPet").GetComponent<Pets>();

    }
    void Start()
    {
        moveName = playerPet.moves[1].moveName;
        ppLeft = playerPet.moves[1].ppCurrent;
        ppTotal = playerPet.moves[1].ppMax;      

        txt = GetComponentInChildren<Text>();
        txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
    }


    private void Update()
    {
        // avoids division by 0
        if (enemyPet.defense == 0)
            enemyPet.defense = 1;


        if (ppLeft == 0)
        {
            moveTwoButton.interactable = false;
        }
        else
            moveTwoButton.interactable = true;

        txt.text = moveName + "  PP: " + ppLeft + "/" + ppTotal;
    }

    public void useMove()
    {

        if (ppLeft == 0)
        {
            Debug.Log("ovetwopp=0");
            return;
        }
        Debug.Log("pp--");
        ppLeft--;
        Debug.Log(ppLeft);



    }

    public int Nip()
    {
        int damage = (playerPet.attack / enemyPet.defense);

        return damage;
    }


    public int Dance()
    {
        int val;
        if (enemyPet.defense <= 10)
            val = 1;
        else
        {
            val = (enemyPet.defense / 10);
        }
        return val;
    }

    public int StickySlap()
    {
        int rand = RNG(1, 100);
        int damage;
        if (rand <= 75)
        {
            damage = (playerPet.attack + 10) / playerPet.defense;
        }
        else
        {
            damage = 0;

        }
        enemyPet.currentHealth -= damage;

        return damage;
    }


    int RNG(int min, int max)
    {
        int num;
        num = UnityEngine.Random.Range(min, max + 1);
        return num;
    }
}
