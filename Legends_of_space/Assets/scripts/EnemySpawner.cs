﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int count;
    public bool canSpawn = true; // 1
    public GameObject enemyPrefab; // 2
    public List<Transform> enemySpawnPositions = new List<Transform>(); // 3
    public float timeBetweenSpawns;
    private List<GameObject> enemyList = new List<GameObject>(); // 5


    void Start()
    {
        //StartCoroutine(SpawnRoutine());
        //count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemy()
    {
        Vector3 randomPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count)].position; // 1
        GameObject enemy = Instantiate(enemyPrefab, randomPosition, enemyPrefab.transform.rotation); // 2
        enemyList.Add(enemy); // 3
        enemy.GetComponent<Enemy>().SetSpawner(this); // 4
    }
    private IEnumerator SpawnRoutine() // 1
    {
        while (canSpawn) // 2
        {
            SpawnEnemy(); // 3
            yield return new WaitForSeconds(timeBetweenSpawns); // 4
        }
    }
    public void DestroyAllEnemy()
    {
        enemyList.Clear();
    }

    public void RemoveEnemyFromList(GameObject enemy, bool hit)
    {

        if (hit == true)
        {

            enemyList.Remove(enemy);
            count++;
        }
        else
        {
            enemyList.Remove(enemy);
        }
    }


}