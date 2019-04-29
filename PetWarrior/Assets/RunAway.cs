using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunAway : MonoBehaviour {
    private getEnemyPet getenemypet;
    public Button runButton;
    public Button feedbackbutton;
    public GameObject feedbackpanel;
    public GameObject startpanel;
    public Text txt;
    // Use this for initialization
    void Start () {
        getenemypet = GameObject.Find("enemyPet").GetComponent<getEnemyPet>();
        runButton.onClick.AddListener(EndBattleText);
    }
    public void EndBattleText()
    {
        feedbackbutton.onClick.RemoveAllListeners();
        startpanel.SetActive(false);
        feedbackpanel.SetActive(true);
        txt.text = "How embarassing...";
        feedbackbutton.onClick.AddListener(EndBattle);
    }
    public void EndBattle()
    {
        // Return to overworld
           if(GameObject.Find("Trainer Pets") != null)
        {
            Destroy(GameObject.Find("Trainer Pets"));
        }
        SceneManager.LoadScene(MySceneManager.instance.lastOverworldScene);

        // SceneManager.LoadScene("startMenu");
        // float newX = 122.3F;
        // float newY = -103.7F;
        // player.transform.position = new Vector2(newX, newY);
        getenemypet.falseflag();

    }

}
