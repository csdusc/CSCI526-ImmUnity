using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBridgeDown : MonoBehaviour
{
    // public float min_x_left = 0.5f, max_x_right = 4.0f, speed = 500f, scaleRate = 0.03f;
    public float min_x_left = 3f, max_x_right = 7f, speed = 500f, scaleRate = 0.03f;
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
            Vector3 pos = transform.localScale;

            // if(pos.x > max_x_right || pos.x < min_x_left){
            //     speed *= -1f; // change direction
            // }

            if(transform.localScale.y > max_x_right){
                speed = -200f; // change direction
            }
            else if(transform.localScale.y < min_x_left){
                speed = 200f; // change direction
            }

            transform.localScale = new Vector3(
                transform.localScale.x , 
                transform.localScale.y + (scaleRate * speed * Time.deltaTime), 
                transform.localScale.z);
        }
    }
}
