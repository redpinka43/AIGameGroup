using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{

    public string levelToLoad;

    public string exitPoint;

    private PlayerController thePlayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == PlayerManager.instance.playerObject)
        {

            // Fade to black and switch scenes
            GUIManager.instance.setFadeState(GUIManager.FadeState.FADING_OUT);

            // counter act loading the trainer into the battle scene by destroying them when you leave the area.
            if (GameObject.Find("Rival") != null)
            {
                Destroy(GameObject.Find("Rival"));
            }
            // Load the next scene
            SceneManager.LoadScene(levelToLoad);
            PlayerController.instance.startPoint = exitPoint;
        }
    }

}
