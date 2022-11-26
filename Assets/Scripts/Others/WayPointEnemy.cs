using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointEnemy : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentIndex = 0;
    public float speed = 3f;
    private SpriteRenderer sprite;
    public bool direction = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    } 

    // Update is called once per frame
    private void Update()
    {
        if(Vector2.Distance(waypoints[currentIndex].transform.position, transform.position) < 0.1f){
            currentIndex ++;
            if(currentIndex >= waypoints.Length){
                currentIndex = 0;
            }

            if(direction == false){
                sprite.flipX = false;
            }
            else{
                sprite.flipX = true;
            }

            direction = !direction;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentIndex].transform.position, Time.deltaTime * speed);
    }
}