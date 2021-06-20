using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManagerScript : MonoBehaviour
{
    public Transform firePoint;
    [SerializeField] private float bulletDelaySeconds, bulletSpeed;
    private float lastBullet;
    public GameObject bulletPrefab;

    private void Start()
    {
        
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Time.time - lastBullet > bulletDelaySeconds)
        {
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bulletSpeed, ForceMode2D.Impulse);

            lastBullet = Time.time;
        }
    }
}
