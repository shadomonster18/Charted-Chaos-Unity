using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static Unity.Collections.AllocatorManager;
using Unity.VisualScripting;

public class chasePlayer : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text loadingText;
    public GameObject black;
    public AudioSource source;
    public GameObject eagle;
    public GameObject Audio;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject system;
    [SerializeField] private float speed;
    public GameObject tear;
    public static float score;
    private float rotationSpeed = 2;
    public float frame = 0;
    public float minTurnSpeed;
    public float maxTurnSpeed;
    public float multiplier = 1;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            a = -1;
        }
        else
        {
            a = 1;
        }
        eagle = GameObject.Find("eagle");
        frame = 0;
        if (system != null)
        {
            system = GameObject.Find("death");
        }
        rb = GetComponent<Rigidbody2D>();

        Vector3 objectPosition = transform.position;

        // Convert world position to viewport position
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(objectPosition);

        if (viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
            viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
            viewportPosition.z > 0) // Ensure the object is in front of the camera
        {
            print("moved " + gameObject.name);
            print(a);
            //transform.position += new Vector3(45 * multiplier * a, 30 * multiplier * a, 0);
            transform.position = new Vector2(transform.position.x + 60 * a, transform.position.y + 33 * a);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && loadingText != null)
        {
            loadingText.text = "you ded\npress R";
        }
        frame += 1;
        if (frame == 1) 
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        if (scoreText != null) 
        {
            scoreText.text = score.ToString();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (black != null)
            {
                black.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (loadingText != null)
            {
                loadingText.text = "Loading...";
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (player != null)
        {
            Vector2 direction = (Vector2)player.position - (Vector2)transform.position;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

            float smoothRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, Time.deltaTime * rotationSpeed);

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, smoothRotation));

            rb.velocity = transform.up * speed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            if (health <= 1)
            {
                if (score == 9)
                {
                    if (tear != null)
                    {
                        tear.GetComponent<SpriteRenderer>().enabled = true;
                        tear.GetComponent<Collider2D>().enabled = true;
                    }
                }
                score += 1;
                if (system != null)
                {
                    Instantiate(system, transform.position, Quaternion.identity);
                }
                Destroy(collision.gameObject);
                if (eagle != null)
                {
                    eagle.GetComponent<AudioSource>().Play();
                    eagle.GetComponent<AudioSource>().time = 0.22f;
                }
                if (Audio != null)
                {
                    Instantiate(Audio);
                }
                Destroy(gameObject);
            }
            else
            {
                health -= 1;
            }
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (frame == 0)
        {
            float a = Random.Range(0, 1);
            
            if (a == 0)
            {
                a = -1;
            }
            else
            {
                a = 1;
            }

            transform.position = new Vector2(transform.position.x + 60 * a, transform.position.y + 33 * a);
            Debug.Log("moved " + gameObject.name);
            //Destroy(gameObject);
        }
    }
    */
}
