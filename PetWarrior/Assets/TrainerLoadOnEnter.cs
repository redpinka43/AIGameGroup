using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerLoadOnEnter : MonoBehaviour {




    public static TrainerLoadOnEnter instance = null;
    void Awake()
    {
        // Object singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
