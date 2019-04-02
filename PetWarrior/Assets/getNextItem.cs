using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getNextItem : MonoBehaviour
{

    private Player player;
    private GameObject itemText;
    private int itemCount;
    public int i;
    Text txt;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        txt = GameObject.Find("ItemInfoText").GetComponent<Text>();
        itemCount = player.items.Count;
        i = 0;
        if (txt != null)
        {
            txt.text = player.items[0];
        }

    }

    public void nextItem()
    {

        if (txt != null)
        {
            i++;
            txt.text = player.items[i % itemCount];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
