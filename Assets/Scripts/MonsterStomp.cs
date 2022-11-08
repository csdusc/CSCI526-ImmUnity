// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MonsterStomp : MonoBehaviour
// {
//    public void OnTriggerEnter2D(Collider2D collision){
//     if(collision.gameObject.tag == "WeakSpot"){
//         Debug.Log("Hereee");
//         Destroy(collision.transform.parent.gameObject);
//     }
//    }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStomp : MonoBehaviour
{
    public GameObject EnemyVariant;
    public GameObject parent, enemy;


   public void OnTriggerEnter2D(Collider2D collision){
    if(collision.gameObject.tag == "PlayerFeet"){
        Vector3 pos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
        GameObject EV = Instantiate(EnemyVariant, pos, Quaternion.identity);
        Destroy(enemy, 0);
        Destroy(EV, 0.1f);
    }
   }
}