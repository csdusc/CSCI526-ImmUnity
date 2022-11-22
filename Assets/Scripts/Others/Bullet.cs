using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    private Health playerHealth;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "BulletEnemy") 
        {
            return;
        }
        else if (target.gameObject.tag == "BulletStopWall")
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void receiveParams(float sp, bool direction)
    {
        if (direction)
        {
            rb.velocity = -transform.right * sp;
        }
        else
        {
            rb.velocity = transform.right * sp;
        }
    }
}
