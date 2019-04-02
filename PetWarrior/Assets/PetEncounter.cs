using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetEncounter : MonoBehaviour {

    public Collider2D coll;
    public EnableBattleScreen battleScreen;

    public int min, max;
    // Use this for initialization
    void Start()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {

            if(RNG(min, max) == 1)
            {
                battleScreen.startBattleScreen();
            }
        }
        else
            Debug.Log("Something else triggered");
    }

    int RNG(int min,int max)
    {
        int num;
        num = Random.Range(min, max + 1);
        return num;
    }
}
