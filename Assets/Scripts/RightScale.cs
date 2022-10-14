using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightScale : MonoBehaviour
{
    public float min_x_left = -1.99f, max_x_right = -0.5f, speed = 2f, scaleRate = 1f;
    private float currSpeed = 2f;
    public bool canMove;
    private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        canMove = true;
    }


    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if(canMove)
        {
            Vector3 pos = transform.localScale;

            if(pos.x > max_x_right){
                currSpeed = -speed;
            }
            else if(pos.x < min_x_left){
                currSpeed = speed;
            }

            transform.localScale = new Vector3(
                pos.x + (scaleRate * currSpeed * Time.deltaTime), 
                pos.y , 
                pos.z);
        }
    }
}
