using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    public static int totalCoins = 0; 
    public static int totalScore = 3;
    private float rotateSpeed = 0.5f;
    public static bool canDropLastBridge = false;

    public ColorChangeScript lastBridgeMarkColor;
    public CoinBarScript coinBar;

    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
        canDropLastBridge = false;

        coinBar.Init();
    }

    void Update() 
    {
        // transform.Rotate(new Vector3(0,rotateSpeed ,0) * Time.deltaTime);
        transform.Rotate(0,rotateSpeed ,0, Space.World);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        //Destroy the coin if Object tagged Player comes in contact with it
        if (c2d.gameObject.CompareTag("Player"))
        {
            //Add coin to counter
            totalCoins++;
            coinBar.AddCoins(1);
            
            if(totalCoins == totalScore)
            {
                lastBridgeMarkColor.ChangeColor();
                canDropLastBridge = true;
            }

            Destroy(gameObject);
        }  
     }    
}