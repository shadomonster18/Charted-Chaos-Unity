using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class PirateMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float slideForce = 5;
    public float dashDuration = 0.2f;
    public float slideTimer;
    public float slideTimerSet;
    public bool AutoFire;
    public bool shouldExplode;
    public float detectionRadius;
    public LayerMask enemyLayer;
    public Rigidbody2D rb;
    public Camera cam;
    public TMP_Text upgradeText;
    public TMP_Text loadingText;


    Vector2 movement;
    Vector2 mousePos;
    Vector2 dashVelocity;
    // Start is called before the first frame update
    void Start()
    {
        slideTimer = slideTimerSet;
        rb = GetComponent<Rigidbody2D>();

        chasePlayer.score = 0;

        StartCoroutine(WaitAndGiveUpgrade("HigherFireRate", 30, 0.2f));
        StartCoroutine(WaitAndGiveUpgrade("HigherSpawnRate", 15, 0.2f));
        StartCoroutine(WaitAndGiveUpgrade("HigherSpeed", 45, 0.2f));
        StartCoroutine(WaitAndGiveUpgrade("HigherSpawnRate", 60, 0.5f));
        StartCoroutine(WaitAndGiveUpgrade("HigherFireRate", 75, 0.18f));
        StartCoroutine(WaitAndGiveUpgrade("AutoFire", 90, 0.25f));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

            if (enemies.Length > 0)
            {
                foreach (Collider2D enemy in enemies)
                {
                    // Destroy the enemy
                    Destroy(enemy.gameObject);
                    Debug.Log("Enemy destroyed: " + enemy.gameObject.name);
                }
            }
            else
            {
                // No enemies found
                Debug.Log("No enemies detected.");
            }
        }
        slideTimer -= Time.deltaTime;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (loadingText != null)
            {
                loadingText.text = "Loading...";
            }
            SceneManager.LoadScene("OnFootCombat");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && slideTimer<=0)
        {
            dashVelocity = new Vector2(movement.x, movement.y) * slideForce;
            StartCoroutine(ResetDash());
            slideTimer = slideTimerSet;
        }
    }


    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed + dashVelocity;

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Eagle"))
        {
            if (collision.gameObject.GetComponent<chasePlayer>().frame > 0)
            {
                Destroy(gameObject);
            }
        }
    }
    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(dashDuration);
        dashVelocity = Vector2.zero;
    }
    IEnumerator WaitAndGiveUpgrade(string upgradeName, float delay, float amount)
    {
   
        yield return new WaitForSeconds(delay);
        /*
        if (upgradeName == "TripleShot")
        {
            upgradeText.text = "+ Triple Shot";
            tripleShot = true;
        }
        if (upgradeName == "SideShot")
        {
            upgradeText.text = "+ Side Shot";
            sideShot = true;
        }
        */
        if (upgradeName == "HigherFireRate")
        {
            upgradeText.text = "+ Higher Fire Rate";
            GetComponent<Shooting>().shootTimerSet -= amount;
        }
        if (upgradeName == "HigherSpeed")
        {
            upgradeText.text = "+ Higher Speed";
            moveSpeed += amount;
        }
        if (upgradeName == "HigherSpawnRate")
        {
            upgradeText.text = "+ Higher Spawn Rate";
            GetComponent<EnemySpawn>().enemyTimer -= amount;
        }
        if (upgradeName == "AutoFire")
        {
            upgradeText.text = "+ Auto Fire";
            GetComponent<Shooting>().AutoFire = true;
        }
    }
}
