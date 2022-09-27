using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscript : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
	  {
            Debug.Log("here");
            Destroy(gameObject);
        }
    }

     public float time = 5; //Seconds to read the text

 
     IEnumerator Start ()
     {
         yield return new WaitForSeconds(time);
         Destroy(gameObject);
     }
}
