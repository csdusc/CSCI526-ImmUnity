using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBridgeUp : MonoBehaviour
{
    // public float min_x_left = 0.5f, max_x_right = 4.0f, speed = 500f, scaleRate = 0.03f;
    public float min_x_left = 10f, max_x_right = 12.5f, speed = 3f, scaleRate = 1f;
    private float currSpeed = 3f;
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
            // Vector3 pos = transform.localScale;
            Vector3 pos = transform.position;

            // if(pos.x > max_x_right || pos.x < min_x_left){
            //     speed *= -1f; // change direction
            // }

            if(pos.y > max_x_right){
                // speed = -200f; // change direction
                currSpeed = -speed;
            }
            else if(pos.y < min_x_left){
                // speed = 200f; // change direction
                currSpeed = speed;
            }

            transform.position = new Vector3(
                pos.x , 
                pos.y + (scaleRate * currSpeed * Time.deltaTime), 
                pos.z);
        }else{
            // Vector3 newPosition = new Vector3(transform.position.x, 12.5f, transform.position.z);
            // transform.position = newPosition;
            StartCoroutine(ScaleDownAnimation(0.5f));
        }
    }

    IEnumerator ScaleDownAnimation(float time)
    {
        float i = 0;
        float rate = 1 / time;

        Vector3 fromScale = transform.position;
        Vector3 toScale = new Vector3(transform.position.x, max_x_right, transform.position.z);
        while (i<1)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
        }

        // body.isKinematic = false;
        // body.constraints = RigidbodyConstraints2D.FreezePosition;
    }
}