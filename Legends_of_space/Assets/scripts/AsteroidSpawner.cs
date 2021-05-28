using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidsPrefab;
    public List<Transform> asteroidsSpawnPositions = new List<Transform>();

    private List<GameObject> asteroidsList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateAllAsteroids();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnAsteroid(Transform t)
    {
        GameObject asteroid = Instantiate(asteroidsPrefab, t.position, asteroidsPrefab.transform.rotation);
        asteroidsList.Add(asteroid);
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
