using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TrainerController : MonoBehaviour {
    public static TrainerController instance = null;

    void Awake()
    {
        // Object singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MySceneManager.instance.destroyableObjects.Add(gameObject);
            MySceneManager.instance.destroyableObjectsSourceScene.Add(SceneManager.GetActiveScene().name);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
