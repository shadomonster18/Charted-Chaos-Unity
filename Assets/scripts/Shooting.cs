using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shootTimerSet;
    public float shootTimer;

    public Transform firePoint;
    public GameObject bullet;
    public AudioSource source;

    public float force = 20;
    // Start is called before the first frame update
    void Start()
    {
        shootTimer = shootTimerSet;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && shootTimer <= 0)
        {
            shootTimer = shootTimerSet;
            Shoot();
        }
        shootTimer -= Time.deltaTime;
    }
    void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * force, ForceMode2D.Impulse);
        source.Play();
    }
}
