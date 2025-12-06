using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shootTimerSet;
    public float shootTimer;
    public float numBalls;
    public bool tripleShot;

    public Transform firePoint;
    public Transform firePoint1;
    public Transform firePoint2;
    public GameObject bullet;
    public AudioSource source;
    public ScreenShake shake;
    public GameObject cannonBall;
    public bool AutoFire;
    public bool cannon;

    public float force = 20;
    // Start is called before the first frame update
    void Start()
    {
        shootTimer = shootTimerSet;
    }

    // Update is called once per frame
    void Update()
    {
        if (!AutoFire)
        {
            if (Input.GetButtonDown("Fire1") && shootTimer <= 0 || Input.GetKeyDown(KeyCode.Space) && shootTimer <= 0)
            {
                shootTimer = shootTimerSet;
                Shoot(1);
            }
            shootTimer -= Time.deltaTime;
        }
        else
        {
            if (Input.GetButton("Fire1") && shootTimer <= 0 || Input.GetKeyDown(KeyCode.Space) && shootTimer <= 0)
            {
                shootTimer = shootTimerSet;
                Shoot(1);
            }
            shootTimer -= Time.deltaTime;
        }
    }
    void Shoot(float spawnNumber)
    {
        shake.StartCoroutine(shake.Shaking());
        GameObject bulletObj = Instantiate(bullet, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.rotation * Vector2.up * force, ForceMode2D.Impulse);
        source.Play();
    }
}
