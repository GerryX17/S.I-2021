using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaTrigger : MonoBehaviour
{
    public string tagPlayer1;
    public string tagPlayer2;
    public GameObject enemyPrefab; 
    public List<Transform> enemySpawnPositions = new List<Transform>(); 
    public float timeBetweenSpawns;
    private List<GameObject> enemyList = new List<GameObject>(); 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(tagPlayer1) || other.CompareTag(tagPlayer2))
        {
            Vector3 randomPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count)].position; 
            GameObject enemy = Instantiate(enemyPrefab, randomPosition, enemyPrefab.transform.rotation); 
            enemyList.Add(enemy); 

        }
    }
}
