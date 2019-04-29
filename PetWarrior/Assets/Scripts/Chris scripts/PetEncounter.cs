using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetEncounter : MonoBehaviour {

    public Collider2D coll;
    public int min, max;

    public static bool justEncounteredAPet;

    // Use this for initialization
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (justEncounteredAPet) {
            justEncounteredAPet = false;
        }
        else if (other.gameObject.name == "Player")
        {
            Debug.Log("Entered Trigger");

            if (RNG(min, max) == 1)
            {
                justEncounteredAPet = true;
                MySceneManager.instance.loadBattleScene();
            }
        }
        else
            Debug.Log("Something else triggered");
    }

    int RNG(int min,int max)
    {
        int num;
        num = Random.Range(min, max + 1);
        Debug.Log(num);

        return num;
    }
}
