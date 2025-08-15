using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    public int numEnemies;
    public float enemyTimer;
    public bool flip = false;
    public float minX, maxX, minY, maxY;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndSpawnEnemies(enemyTimer, numEnemies));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator WaitAndSpawnEnemies(float delay, int numEagles)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < numEagles; i++)
        {
            yield return new WaitForSeconds(enemyTimer);
            AddEnemy();
        }
    }
    void AddEnemy()
    {
    Vector2 randomPosition = new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));

        int a = Random.Range(0, 1);
        if (a == 0)
        {
            a = -1;
        }
        else
        {
            a = 1;
        }
        if (flip)
        {
            randomPosition = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
        }
        GameObject eagleInstance = Instantiate(Enemy, randomPosition, Quaternion.identity);
        SpriteRenderer eagleSpriteRenderer = eagleInstance.GetComponent<SpriteRenderer>();
        if (eagleSpriteRenderer != null)
        {
            eagleSpriteRenderer.enabled = false;
        }
    }
}
