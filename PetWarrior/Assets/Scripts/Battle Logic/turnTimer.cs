using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnTimer : MonoBehaviour {

    public bool playerPetTurn;

    public void TurnCheck()
    {
        if (playerPetTurn == true);
        {
            /// then the player takes it's turn
            /// 


            playerPetTurn = false;
        }
    }
    public bool GivePlayerTurn()
    {
        playerPetTurn = true;
        return true;
    }

    public bool GiveEnemyTurn()
    {
        playerPetTurn = false;
        return false;
    }
}
