using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public static EnemiesSpawner instance;

    public GameObject enemyPrefab;
    public GameObject nebulaPrefab;
    public GameObject finalBossPrefab;

    public List<Transform> enemySpawnPositions = new List<Transform>();
    private List<bool> hasAlreadySpawnedHere= new List<bool>();

    private int enemySpawnCount;

    private bool canNebulaSpawn;
    
    private int SpawnID;

    public bool canEnemySpawn;

    private bool triggerLastFight;

    private GameObject currentEnemy;
    private GameObject currentNebula;

    public int enemyDeathCount;

    public int timeBetweenSpawns;


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        triggerLastFight = false;

        SpawnID = 0;

        canNebulaSpawn = true;

        canEnemySpawn = false;

        enemyDeathCount = 0;

        enemySpawnCount = enemySpawnPositions.Count;

        initBoolList();

        StartCoroutine(spawnRoutine());
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyDeathCount == 5 && triggerLastFight == false)
        {
            triggerLastFight = true;
            finalFight();
        }
    }

    private IEnumerator spawnRoutine()
    {
        while (true) { 
            if ( canNebulaSpawn == true && enemyDeathCount < 5 )
            {
                initNebulaEnemy();
            }

            if ( canEnemySpawn == true && enemyDeathCount < 5 )
            {
                currentEnemy = SpawnEnemy(SpawnID);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }



            yield return null;
        }
    }

    private GameObject SpawnEnemy(int rand)
    {
        Transform t = enemySpawnPositions[rand];

        GameObject enemy = Instantiate(enemyPrefab, t.position, enemyPrefab.transform.rotation);

        canEnemySpawn = false;

        return enemy;
    }

    private GameObject SpawnNebula(int rand)
    {
        Transform t = enemySpawnPositions[rand];
        Vector3 nebula_pos = new Vector3(t.position.x, nebulaPrefab.transform.position.y, t.position.z);

        GameObject nebula = Instantiate(nebulaPrefab, nebula_pos, nebulaPrefab.transform.rotation);

        return nebula;
    }

    public void removeEnemy()
    {
        Destroy(currentEnemy);

        enemyDeathCount += 1;

        Destroy(currentNebula);

        canNebulaSpawn = true;
    }

    private void initBoolList()
    {
        for (int i = 0; i < enemySpawnCount; i++)
        {
            hasAlreadySpawnedHere.Add(false);
        }
    }

    private int findNextFreeSpawnPoint()
    {
        List<int> emptySpawnPositions = new List<int>();
        int index = 0;

        foreach (bool b in hasAlreadySpawnedHere)
        {
            if (b == false)
                emptySpawnPositions.Add(index);
            index += 1;
        }

        return emptySpawnPositions[Random.Range(0, emptySpawnPositions.Count)];
    }

    private void initNebulaEnemy()
    {
        canNebulaSpawn = false;

        SpawnID = Random.Range(0, enemySpawnCount);

        if (hasAlreadySpawnedHere[SpawnID])
        {
            findNextFreeSpawnPoint();
        }

        hasAlreadySpawnedHere[SpawnID] = true;
        currentNebula = SpawnNebula(SpawnID);
    }

    private void finalFight()
    {
        Vector3 final_boss_pos = new Vector3( 50f, 11f, 50f);
        GameObject planet = Instantiate(finalBossPrefab, final_boss_pos, finalBossPrefab.transform.rotation);

        SoundManager.Instance.playFinalBoss();
    }

}
