using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving3 : MonoBehaviour
{
    public float minx, maxx, speed;

    private Vector3 dir = Vector3.left;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        minx = 10f;
        maxx = 12f;
    }

    // Update is called once per frame
    void Update()
    {
         MoveEnemy();   
    }

    void MoveEnemy()
    {
            transform.Translate(dir*speed*Time.deltaTime);
 
            if(transform.position.x <= minx){
                dir = Vector3.right;
            }else if(transform.position.x >= maxx){
                dir = Vector3.left;
            }
    }
}
