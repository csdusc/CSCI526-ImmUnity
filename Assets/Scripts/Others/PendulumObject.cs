using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumObject : MonoBehaviour
{
    Rigidbody2D rgb2d;
    public float moveSpeed;
    public float leftAngle;
    public float rightAngle;
    bool movingClockwise;
    // Start is called before the first frame update
    void Start()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        movingClockwise = true;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();

        if (movingClockwise)
        {
            rgb2d.angularVelocity = moveSpeed;
        }

        if (!movingClockwise)
        {
            rgb2d.angularVelocity = -1 * moveSpeed;
        }
    }

    public void ChangeDirection()
    {
        if (transform.rotation.z > rightAngle)
        {
            movingClockwise = false;
        }

        if (transform.rotation.z < leftAngle)
        {
            movingClockwise = true;
        }
    }
}
