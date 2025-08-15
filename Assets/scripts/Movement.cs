using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public GameObject Eagle;
    public Transform mainIsland;
    public GameObject black;
    public TMP_Text loadingText;
    public TMP_Text upgradeText;
    public ScreenShake shake;
    public bool tripleShot;
    public bool sideShot;
    public float num_eagles;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public float eagleTimer;
    public float eagleTimerSet;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject cannonball;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnPoint_1;
    [SerializeField] private Transform spawnPoint_2;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float pastSpeed = 9f;
    [SerializeField] private float minSpeed = 2f;
    [SerializeField] private float Deceleration = 150f;
    [SerializeField] private float Acceleration = 150f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float rotationAcceleration = 150f;
    [SerializeField] private float rotationDeceleration = 200f;
    [SerializeField] private float currentRotationSpeed = 0f;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip cannonClip;
    [SerializeField] private float shootTimer;
    [SerializeField] private float shootTimerSet;
    public bool wave = false;
    public float EaglesPerWave = 10;
    private float frame = 0;
    private float EnemySpawnDelay = 2f;

    void Start()
    {
        maxSpeed = moveSpeed;

        StartCoroutine(WaitAndSpawnEnemies(1f, 10000));
        StartCoroutine(WaitAndGiveUpgrade("TripleShot", 50, 0.2f));
        StartCoroutine(WaitAndGiveUpgrade("SideShot", 80, 0.2f));
        StartCoroutine(WaitAndGiveUpgrade("HigherFireRate", 15, 0.2f));
        StartCoroutine(WaitAndGiveUpgrade("HigherSpawnRate", 30, 0.7f));
        StartCoroutine(WaitAndGiveUpgrade("HigherSpawnRate", 80, 0.4f));
        StartCoroutine(WaitAndGiveUpgrade("HigherSpawnRate", 110, 0.2f));
        StartCoroutine(WaitAndGiveUpgrade("HigherSpawnRate", 110, 0.2f));
        //(AllWaves(20, 5, 20));
        eagleTimerSet = eagleTimer;
        rb = GetComponent<Rigidbody2D>();
        shootTimerSet = shootTimer;
        chasePlayer.score = 0;
        /*
        for (int i = 0; i < num_eagles; i++)
        {
            Vector2 randomPosition = new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));

            Instantiate(Eagle, randomPosition, Quaternion.identity);
        }
        */
    }
    IEnumerator WaitAndGiveUpgrade(string upgradeName, float delay, float amount)
    {
        yield return new WaitForSeconds(delay);
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
        if (upgradeName == "HigherFireRate")
        {
            upgradeText.text = "+ Higher Fire Rate";
            shootTimerSet -= amount;
        }
        if (upgradeName == "HigherSpawnRate")
        {
            upgradeText.text = "+ Higher Spawn Rate";
            EnemySpawnDelay -= amount;
        }
    }
    public void AddEagle()
    {
        Vector2 randomPosition = new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));

        GameObject eagleInstance = Instantiate(Eagle, randomPosition, Quaternion.identity);
        SpriteRenderer eagleSpriteRenderer = eagleInstance.GetComponent<SpriteRenderer>();
        if (eagleSpriteRenderer != null)
        {
            eagleSpriteRenderer.enabled = false;
        }
    }
    IEnumerator AllWaves(float delayBetweenWaves, int numWaves, int numEnemies)
    {
        for (int i = 0; i < numWaves; i++)
        {
            //StartCoroutine(WaitAndSpawnEnemies(3, numEnemies, 0.7f));
            yield return new WaitForSeconds(delayBetweenWaves);
        }
    }
    IEnumerator WaitAndSpawnEnemies(float delay, int numEagles)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < numEagles; i++)
        {
            yield return new WaitForSeconds(EnemySpawnDelay);
            AddEagle();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //wave = true;
        }
        if (wave)
        {
            wave = false;

            for (int i = 0; i < EaglesPerWave; i++)
            {
                AddEagle();
            }
        }
        eagleTimer -= Time.deltaTime;
        frame += 1;
        if (Input.GetKeyDown(KeyCode.R) && black != null)
        {

            black.GetComponent<SpriteRenderer>().enabled = true;
            if (loadingText != null)
            {
                loadingText.text = "Loading...";
            }
            SceneManager.LoadScene("SampleScene");
        }
        shootTimer -= Time.deltaTime;//make the timer move down

        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);//move player in the direction they're facing
        if (eagleTimer <= 0)
        {
            eagleTimer = eagleTimerSet;

        }
        //rotation controls
        if (Input.GetKey(KeyCode.A))
        {
            currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, rotationSpeed, rotationAcceleration * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, -rotationSpeed, rotationAcceleration * Time.deltaTime);
        }
        else
        {
            currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, 0f, rotationDeceleration * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, minSpeed, Deceleration * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, pastSpeed, Acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.MoveTowards(maxSpeed, 0f, Acceleration * Time.deltaTime);
        }


        transform.Rotate(Vector3.forward * currentRotationSpeed * Time.deltaTime);//rotate player

        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);//make it so the camera is always in the same position as the player

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (shootTimer <= 0)
            {
                shootTimer = shootTimerSet;
                source.PlayOneShot(cannonClip);
                CreateBall(0, spawnPoint);
                if (tripleShot)
                {
                    CreateBall(-15, spawnPoint);
                    CreateBall(15, spawnPoint);
                }

                if (sideShot)
                {
                    // left side shooting
                    CreateBall(0, spawnPoint_1);

                    // right side shooting
                    CreateBall(0, spawnPoint_2);
                }

                shake.StartCoroutine(shake.Shaking());
            }
        }
    }
    void CreateBall(float offset, Transform point)
    {
        Quaternion offsetRotation = Quaternion.AngleAxis(offset, Vector3.forward);

        Quaternion finalRotation = point.rotation * offsetRotation;

        Instantiate(cannonball, point.position, finalRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("map"))
        {
            rb.AddForce(-transform.up * 5, ForceMode2D.Impulse);
        }
        if (collision.gameObject.CompareTag("Eagle"))
        {
            if (collision.gameObject.GetComponent<chasePlayer>().frame > 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
