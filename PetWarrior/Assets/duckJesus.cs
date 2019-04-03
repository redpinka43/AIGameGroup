using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckJesus : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        Debug.Log("This will be called every frame");
        if (Input.GetKeyDown("space") && other.tag == "TalkCylinder")
        {
            Debug.Log("Talking");
        }
    }
}
