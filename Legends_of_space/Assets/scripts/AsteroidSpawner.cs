using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public static AsteroidSpawner instance;

    public Asteroid asteroidsPrefab;
    public List<Transform> asteroidsSpawnPositions = new List<Transform>();
    private List<bool> isSpawnPointOccupied = new List<bool>();

    private List<Asteroid> asteroidsList = new List<Asteroid>();

    public bool canSpawn;
    public int initialAsteroidCount;
    public int maxCount;

    private int asteroidSpawnCount;

    public int currentNumOfAsteroid;

    public float lastTimeAsteroidSpawned;
    public float timeBetweenSpawns;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;

        asteroidSpawnCount = asteroidsSpawnPositions.Count;

        currentNumOfAsteroid = 0;

        initBoolList();

        lastTimeAsteroidSpawned = 0.0f;

        maxCount = 8;

        timeBetweenSpawns = 4.0f;

        fillRandomAsteroids(initialAsteroidCount);

        StartCoroutine(spawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        currentNumOfAsteroid = asteroidsList.Count;
    }

    private IEnumerator spawnRoutine()
    {
        while(canSpawn && asteroidsList.Count < maxCount)
        {
            int randomPosition = Random.Range(0, asteroidSpawnCount);
            if ( isSpawnPointOccupied[randomPosition])
            {
                randomPosition = findNextFreeSpawnPoint(randomPosition);
            }
            SpawnAsteroid(randomPosition);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private Asteroid SpawnAsteroid(int rand)
    {
        Transform t = asteroidsSpawnPositions[rand];

        Asteroid asteroid = Instantiate<Asteroid>(asteroidsPrefab, t.position, asteroidsPrefab.transform.rotation);

        asteroid.id = rand;

        isSpawnPointOccupied[rand] = true;

        asteroidsList.Add(asteroid);

        return asteroid;
    }

    public void RemoveAsteroid(Asteroid asteroid)
    {
        isSpawnPointOccupied[asteroid.id] = false;

        asteroidsList.Remove(asteroid);

        asteroidsList.TrimExcess();
    }

    public void CreateAllAsteroids() 
    {
        for(int i = 0; i < asteroidSpawnCount; i++)
        {
            SpawnAsteroid(i);
        }
    }

    private void initBoolList()
    {
        for (int i = 0; i < asteroidsSpawnPositions.Count; i++)
        {
            isSpawnPointOccupied.Add(false);
        }
    }

    public void DestroyAllAsteroids()
        {
            foreach (Asteroid asteroid in asteroidsList)
            {
                RemoveAsteroid(asteroid);
            }

            initBoolList();

            asteroidsList.Clear();
        }


    private int findNextFreeSpawnPoint(int asteroid_n)
    {
        List<int> emptySpawnPositions = new List<int>();
        int index = 0;

        foreach (bool b in isSpawnPointOccupied)
        {
            if (b == false)
                emptySpawnPositions.Add(index);
            index += 1;
        }

        return emptySpawnPositions[Random.Range(0, emptySpawnPositions.Count)];
    }


    private void fillRandomAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int asteroid_n = Random.Range(0, asteroidSpawnCount);
            if(isSpawnPointOccupied[asteroid_n] == true)
            {
                findNextFreeSpawnPoint(asteroid_n);
            }
            
            SpawnAsteroid(asteroid_n);
        }
    }


    }
