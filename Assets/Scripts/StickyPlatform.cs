using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StickyPlatform : MonoBehaviour
{
    // public GameObject portal1;
    // public GameObject portal2;
    // public GameObject entry;
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player")){
            collision.gameObject.transform.SetParent(transform);
    
        }
        // Destroy(portal1);
        // Destroy(portal2);
        // Destroy(entry);

    }

    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player")){
            collision.gameObject.transform.SetParent(null);
        }
    }
}
