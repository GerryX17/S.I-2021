using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidsPrefab;
    public List<Transform> asteroidsSpawnPositions = new List<Transform>();

    private List<GameObject> asteroidsList = new List<GameObject>();

    public bool canSpawn = true;

    public float lastTimeAsteroidSpawned;
    public float timeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        //CreateAllAsteroids();

        lastTimeAsteroidSpawned = 0.0f;

        for(int i = 0; i < 4; i++)
        {
            Transform t = asteroidsSpawnPositions[Random.Range(0, asteroidsSpawnPositions.Count)];
            GameObject asteroid = SpawnAsteroid(t);
        }

        StartCoroutine(spawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnRoutine()
    {
        while(canSpawn)
        {
            Transform t = asteroidsSpawnPositions[Random.Range(0, asteroidsSpawnPositions.Count)];
            GameObject asteroid = SpawnAsteroid(t);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private GameObject SpawnAsteroid(Transform t)
    {
        GameObject asteroid = Instantiate(asteroidsPrefab, t.position, asteroidsPrefab.transform.rotation);
        asteroidsList.Add(asteroid);

        return asteroid;
    }

    public void RemoveAsteroidFromList(GameObject asteroid)
    {
        asteroidsList.Remove(asteroid);
    }

    public void CreateAllAsteroids() {
        foreach (Transform t in asteroidsSpawnPositions)
        {
            // SpawnAsteroid(t);
            GameObject asteroid = Instantiate(asteroidsPrefab, t.position, asteroidsPrefab.transform.rotation);
            asteroidsList.Add(asteroid);
        }
    }

    public void DestroyAllAsteroids()
        {
            foreach (GameObject asteroid in asteroidsList)
            {
                Destroy(asteroid);
            }

            asteroidsList.Clear();
        }

    }
