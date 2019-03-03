using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Collider2D coll;
    private PlayerController thePlayer;
    private Vector3 ydown;
    private GameObject chad;

    // Use this for initialization
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        //hardcoded to find a specificly named NPC. Change later.
        chad = GameObject.Find("npc_chad 1");
        ydown = new Vector3(0.0f, 20.0f, 0.0f);
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
        // move towards player when sighted
        chad.transform.position = Vector2.MoveTowards(transform.position, thePlayer.transform.position + ydown, 40 * Time.deltaTime);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("ok2");
    }
}

