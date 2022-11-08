using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float shootingTime;
    public float speed;
    public bool direction;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > shootingTime)
        {
            shootingTime = Time.time + 1.0f;
            GameObject obj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            obj.GetComponent<Bullet>().receiveParams(speed, direction);
        }
    }
}
