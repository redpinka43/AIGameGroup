using UnityEngine;

public class Example : MonoBehaviour
{
    // Float a rigidbody object a set distance above a surface.

    Rigidbody2D rb2D;
    Vector2 startcast;
    Vector2 endcast;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        startcast = transform.position;
        
        
        endcast.x = startcast.x;
        endcast.y = startcast.y - 60.0f;
        Debug.DrawLine(startcast, endcast, Color.yellow, 20000, false);

        RaycastHit2D hit = Physics2D.Raycast(startcast, endcast, 60.0f);

        // If it hits something...
        if (hit.collider.gameObject.tag == "Player")
        {
           
            Debug.Log("OK");
        }
    }
}