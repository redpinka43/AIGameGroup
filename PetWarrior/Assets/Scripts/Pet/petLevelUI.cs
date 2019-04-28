using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petLevelUI : MonoBehaviour
{
    Text txt;
    public Pets pet;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        txt.text = "LVL:" + pet.level.ToString();
    }
}
