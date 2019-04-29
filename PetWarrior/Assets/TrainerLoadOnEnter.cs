using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerLoadOnEnter : MonoBehaviour {

    public static bool triggerCheck;

    void OnTriggerEnter2D(Collider2D col)
    {

        
            // Object singleton
            if (triggerCheck == false)
            {
                DontDestroyOnLoad(gameObject);
                triggerCheck = true;
            }
            
        }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerCheck = false;
    }
}
