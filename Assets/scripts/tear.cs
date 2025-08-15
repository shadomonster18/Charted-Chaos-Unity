using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tear : MonoBehaviour
{
    public Transform tearSpawn;
    public bool shouldSwitch = true;
    public string sceneName = "next";
    // Start is called before the first frame update
    void Start()
    {
        if (tearSpawn != null)
        {
            transform.position = tearSpawn.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && shouldSwitch)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
   
}

