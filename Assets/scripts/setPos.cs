using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPos : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 2f;
    public float offSet = 0;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 newPos = new Vector3(player.position.x, player.position.y + offSet, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }
}
