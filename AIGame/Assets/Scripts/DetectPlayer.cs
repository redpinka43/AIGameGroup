using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Collider2D coll;
    private PlayerController thePlayer;
    // Use this for initialization
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player triggered");
            thePlayer.canMove = false;
        }
        else
            Debug.Log("Something else triggered");
    }
    void OnTriggerStay2D(Collider2D other)
    {
       // Debug.Log("ok1");
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("ok2");
    }
}

