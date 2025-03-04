using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float spawnTerm = 5.0f;
    public float fasterEverySpawn = 0.05f;
    public float minSpawnTerm = 1;
    public TextMeshProUGUI scoreText;

    float timeAfterLastSpawn;
    float score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeAfterLastSpawn = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterLastSpawn += Time.deltaTime;
        score += Time.deltaTime;

        if (timeAfterLastSpawn > spawnTerm)
        {
            timeAfterLastSpawn -= spawnTerm;

            SpawnEnemy();

            spawnTerm -= fasterEverySpawn;
            if (spawnTerm < minSpawnTerm)
            {
                spawnTerm = minSpawnTerm;
            }
        }

        scoreText.text = ((int)score).ToString();
    }

    void SpawnEnemy()   // Add this method
    {
        float x = Random.Range(-9f, 9f);
        float y = Random.Range(-4.5f, 4.5f);

        GameObject obj = GetComponent<ObjectPool>().Get();
        obj.transform.position = new Vector3(x, y, 0);
        obj.GetComponent<EnemyController>().Spawn(player);
    }
}
