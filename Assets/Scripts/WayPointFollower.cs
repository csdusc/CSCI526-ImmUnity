using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentIndex = 0;
    public float speed = 3f;

    // Update is called once per frame
    private void Update()
    {
        if(Vector2.Distance(waypoints[currentIndex].transform.position, transform.position) < 0.1f){
            currentIndex ++;
            if(currentIndex >= waypoints.Length){
                currentIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentIndex].transform.position, Time.deltaTime * speed);
    }
}
