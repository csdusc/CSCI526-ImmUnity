using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightBridge_1_L2 : MonoBehaviour
{
    public float min_x_left, max_x_right, speed, scaleRate = 1f;
    private float currSpeed;
    public bool canMove;
    private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        canMove = true;
        currSpeed = speed;
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
