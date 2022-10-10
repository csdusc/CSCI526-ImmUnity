using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public float minx, maxx, speed;

    private Vector3 dir = Vector3.left;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        minx = 17f;
        maxx = 19f;
    }

    // Update is called once per frame
    void Update()
    {
         MoveEnemy();   
    }

    void MoveEnemy()
    {
            // Vector3 temp = transform.position;
            // temp.x += speed * Time.deltaTime;

            // if(temp.x > maxx){
            //     speed *= -1f;
            // }
            // else if(temp.x < minx){
            //     speed *= -1f;
            // }

            // transform.position = temp;

            transform.Translate(dir*speed*Time.deltaTime);
 
            if(transform.position.x <= minx){
                dir = Vector3.right;
            }else if(transform.position.x >= maxx){
                dir = Vector3.left;
            }
    }
}
