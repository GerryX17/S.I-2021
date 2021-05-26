using System.Collections;
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

    //public TextMeshProUGUI countText;
    public GameObject winTextObject;
    //public GameObject loseTextObject;
    public GameObject restartButton;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        count = 0;
        //countText.text = "Count: " + count.ToString();
        restartButton.gameObject.SetActive(false);
        winTextObject.SetActive(false);
        //loseTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        Vector3 randomPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count)].position; // 1
        GameObject sheep = Instantiate(enemyPrefab, randomPosition, enemyPrefab.transform.rotation); // 2
        enemyList.Add(sheep); // 3
        sheep.GetComponent<Enemy>().SetSpawner(this); // 4
    }
    private IEnumerator SpawnRoutine() // 1
    {
        while (canSpawn) // 2
        {
            SpawnEnemy(); // 3
            yield return new WaitForSeconds(timeBetweenSpawns); // 4
        }
    }
    public void DestroyAllSheep()
    {
        enemyList.Clear();
    }

    public void RemoveEnemyFromList(GameObject enemy, bool hit)
    {

        if (hit == true)
        {

            enemyList.Remove(enemy);
            count++;
            //countText.text = "Count: " + count.ToString();
            //endgame();
        }
        else
        {
            //restartButton.gameObject.SetActive(true);
            //loseTextObject.SetActive(true);
            enemyList.Remove(enemy);
        }
    }


}
