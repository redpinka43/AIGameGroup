using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthBarScript : MonoBehaviour
{
    [SerializeField] private float fillAmount;

    [SerializeField] private Image filler;

    private Pets playerPet;

    // Start is called before the first frame update
    void Start()
    {
        playerPet = GameObject.Find("playerPet").GetComponent<Pets>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    // updates how far the health bar should be filled
    private void HandleBar()
    {
        // current health / health because this will give a decimal to the fill bar
        // typecasted as floats to get decimals
        fillAmount = (float)playerPet.currentHealth / (float)playerPet.health;
        if (fillAmount != filler.fillAmount)
        {
            filler.fillAmount = fillAmount;
        }
    }
}
