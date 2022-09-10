using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float minx = 2f, maxx = 3.5f, speed = 5f;

    private bool canMove;
    private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        speed = 4f;

        if(Random.Range(0, 2) > 0){
            speed *= -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveBox();

        if(Input.GetMouseButtonDown(0))
        {
            Dropbox();
        }
    }

    void Dropbox()
    {
        canMove = false;
        body.gravityScale = 1f;
    }

    void MoveBox()
    {
        if(canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;

            if(temp.x > maxx){
                speed *= -1f;
            }
            else if(temp.x < minx){
                speed *= -1f;
            }

            transform.position = temp;
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Water")
        {
            body.isKinematic = false;
            body.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
}
