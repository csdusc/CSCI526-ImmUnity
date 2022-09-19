using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    
    public static int totalCoins = 0; 

    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        //Destroy the coin if Object tagged Player comes in contact with it
        if (c2d.gameObject.CompareTag("Player"))
        {
            //Add coin to counter
            totalCoins++;
            Destroy(gameObject);
        }
        
     }
    
}