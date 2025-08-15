using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonblast : MonoBehaviour
{
    public Rigidbody2D rb;
    public float blastForce = 100;
    public bool destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * blastForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 1000 || transform.position.y < -1000 || transform.position.x > 1000 || transform.position.x < -1000)
        {
            Debug.Log("destroyed cannonball");
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}
