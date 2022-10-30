using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStomp : MonoBehaviour
{
   public void OnTriggerEnter2D(Collider2D collision){
    if(collision.gameObject.tag == "WeakSpot"){
        Debug.Log("Hereee");
        Destroy(collision.transform.parent.gameObject);
    }
   }
}
