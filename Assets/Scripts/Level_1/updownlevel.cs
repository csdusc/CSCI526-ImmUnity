using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updownlevel : MonoBehaviour
{
    

    public float min_x_left = 2.5f, max_x_right = 6f, speed = 1f, scaleRate = 1f;
    private float currSpeed = 5f;
    public bool canMove;
    private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        // body.gravityScale = 0f;
    }


    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if(canMove)
        {
            Vector3 pos = transform.position;

            // if(pos.x > max_x_right || pos.x < min_x_left){
            //     speed *= -1f; // change direction
            // }

            if(pos.y > max_x_right){
                //speed = -20f; // change direction
                currSpeed = -speed;
            }
            else if(pos.y < min_x_left){
                //speed = 20f; // change direction
                currSpeed = speed;
            }

            transform.position = new Vector3(
                pos.x , 
                pos.y + (scaleRate * currSpeed * Time.deltaTime), 
                pos.z);
        }
    }

}
