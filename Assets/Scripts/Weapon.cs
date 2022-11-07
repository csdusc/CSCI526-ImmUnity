using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float shootingTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > shootingTime)
        {
            shootingTime = Time.time + 1.0f;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
