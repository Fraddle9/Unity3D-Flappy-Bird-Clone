using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstacle;
    public float maxTime;
    float timer;
    public float maxY;
    public float minY;
    float randomY;
    // Start is called before the first frame update
    void Start()
    {
        //InstantiateObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameOver && GameManager.gameStarted)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                randomY = Random.Range(minY, maxY);
                InstantiateObstacle();
                timer = 0;
            }
        }
        
    }

    public void InstantiateObstacle()
    {
        GameObject newobstacle = Instantiate(obstacle);
        newobstacle.transform.position = new Vector2(transform.position.x, randomY);
    }
}
