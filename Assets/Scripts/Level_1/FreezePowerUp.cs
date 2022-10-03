using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePowerUp : MonoBehaviour
{
    void Awake()
    {
        //Make Collider2D as trigger 
        GetComponent<Collider2D>().isTrigger = true;
    }

    void Update() 
    {
        // transform.Rotate(new Vector3(0,rotateSpeed ,0) * Time.deltaTime);
        //transform.Rotate(0,rotateSpeed ,0, Space.World);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        //Destroy the coin if Object tagged Player comes in contact with it
        if (c2d.gameObject.CompareTag("Player"))
        {
            //Add coin to counter
            Destroy(gameObject);
           
        }

        
     }  

}
