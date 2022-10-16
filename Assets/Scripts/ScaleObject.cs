using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    [SerializeField] private float minSize, maxSize, speed, scaleRate;
    [SerializeField] private bool scaleX;
    private float currSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.localScale;

        if(scaleX)
        {
            if(transform.localScale.x > maxSize){
                currSpeed = -speed; // change direction
            }
            else if(transform.localScale.x < minSize){
                currSpeed = speed; // change direction
            }

            transform.localScale = new Vector3(
                transform.localScale.x + (scaleRate * currSpeed * Time.deltaTime), 
                transform.localScale.y, 
                transform.localScale.z);
        }
        else
        {
            if(transform.localScale.y > maxSize){
                currSpeed = -speed; // change direction
            }
            else if(transform.localScale.y < minSize){
                currSpeed = speed; // change direction
            }

            transform.localScale = new Vector3(
                transform.localScale.x, 
                transform.localScale.y + (scaleRate * currSpeed * Time.deltaTime), 
                transform.localScale.z);
        }
    }
}
