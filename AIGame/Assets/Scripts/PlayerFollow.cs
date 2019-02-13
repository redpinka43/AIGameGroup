using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour
{

    //public Transform target;//set target from inspector instead of looking in Update
    public float speed = 300f;
    private GameObject Player;
    private Vector3 ypos;
    private Vector3 yplus;
    private Vector3 yup;
    private Vector3 ydown;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        ypos = new Vector3(-20.0f, 0.0f, 0.0f);
        yplus = new Vector3(20.0f, 0.0f, 0.0f);
        yup = new Vector3(0.0f, -20.0f, 0.0f);
        ydown = new Vector3(0.0f, 20.0f, 0.0f);
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position + ypos, speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < -0.1f)
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position + yplus, 900 * Time.deltaTime);
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical") > 0.1f)
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position + yup, 900 * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < -0.1f)
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position + ydown, 900 * Time.deltaTime);

    }

    


}
