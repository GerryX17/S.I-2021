using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaTrigger : MonoBehaviour
{
    private FiducialController fc1;
    private FiducialController fc2;

    public string tagPlayer1;
    public string tagPlayer2;

    private Transform nebula;
    private Vector3 nebula_pos;

    private Transform player1;
    private Transform player2;

    private bool isPlayer1Close = false;
    private bool isPlayer2Close = false;

    public GameObject redLaserPrefab;
    public GameObject blueLaserPrefab;

    public GameObject enemyPrefab; 
    public List<Transform> enemySpawnPositions = new List<Transform>();

    private GameObject enemy;

    public float timeBetweenSpawns;

    private bool hasSpawned = false;

    private List<GameObject> enemyList = new List<GameObject>();

    private void Start()
    {
        nebula = GetComponent<Transform>();
        nebula_pos = nebula.position;

        GameObject p1 = GameObject.FindGameObjectsWithTag (tagPlayer1)[0];
        player1 = p1.transform;

        GameObject p2 = GameObject.FindGameObjectsWithTag(tagPlayer2)[0];
        player2 = p2.transform;
    }

    private void Update()
    {
        
        float dist1 = Vector3.Distance(player1.position, nebula_pos);
        if (dist1 < 30)
            isPlayer1Close = true;
        else
            isPlayer1Close = false;

        float dist2 = Vector3.Distance(player2.position, nebula_pos);
        if (dist2 < 30)
            isPlayer2Close = true;
        else
            isPlayer2Close = false;


        if ( (isPlayer1Close && isPlayer2Close) && !hasSpawned )
        {
            hasSpawned = true;

            Invoke("spawnEnemy", 1);

            lookAtEnemy();

            shootAtEnemy();

            Invoke("playersCanMove", 3);
        }

    }

    private void spawnEnemy()
    {
        Vector3 randomPosition = enemySpawnPositions[Random.Range(0, enemySpawnPositions.Count)].position;
        enemy = Instantiate(enemyPrefab, randomPosition, enemyPrefab.transform.rotation);
        enemyList.Add(enemy);
    }

    private void lookAtEnemy()
    {
        // player1 stop moving, look at enemy and shoot at it
        GameObject p1 = GameObject.FindGameObjectsWithTag(tagPlayer1)[0];
        fc1 = p1.GetComponent<FiducialController>();
        fc1.fighting = true;
        fc1.transform.LookAt(nebula, new Vector3(0, 1, 0));

        // player2 stop moving, look at enemy and shoot at it
        GameObject p2 = GameObject.FindGameObjectsWithTag(tagPlayer2)[0];
        fc2 = p2.GetComponent<FiducialController>();
        fc2.fighting = true;
        fc2.transform.LookAt(nebula, new Vector3(0, 1, 0));
    }

    private void shootAtEnemy()
    {
        Instantiate(redLaserPrefab, player1.position, Quaternion.identity);
        Instantiate(blueLaserPrefab, player2.position, Quaternion.identity);
        SoundManager.Instance.PlayShootClip();

        // los disparos avanzan y paran al llegar a nebula_pos y luego hacemos animación de explosión
        Destroy(enemy, 5);
        enemyList.Remove(enemy);
        SoundManager.Instance.PlayShipHitClip();
    }

    private void playersCanMove()
    {
        // both players can move again
        fc1.fighting = false;
        fc2.fighting = false;
    }
}
