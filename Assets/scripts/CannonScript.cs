using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CannonScript : MonoBehaviour
{
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1; // Unpause the game
            text.text = "";
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0; // Pause the game
            text.text = "Game Paused";
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("time");
            TogglePause();
        }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
