using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBattleScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            // Save overworld position
            PlayerController.instance.saveOverworldPosition();

            SceneManager.LoadScene("battleScreen");
        }
          
    }
    
}
