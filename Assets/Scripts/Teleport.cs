using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // public GameObject portal;
    // private GameObject player;

 
    // void Start()
    // {
    //     player = GameObject.FindWithTag("Player");
    // }
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.tag == "Player")
    //     {
    //         player.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
    //     }
    
    // }

    public GameObject portal1;
    public GameObject portal2;

    private GameObject player;
    
    public System.Random ran = new System.Random();
    public int x;
 
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            x = ran.Next(0, 10);
            if ((x%2) == 0)
            {
                player.transform.position = new Vector2(portal1.transform.position.x, portal1.transform.position.y);
                

            }
            else
            {
                player.transform.position = new Vector2(portal2.transform.position.x, portal2.transform.position.y);
            }
        }
        Destroy(portal1);
        Destroy(portal2);
        
    }
}
