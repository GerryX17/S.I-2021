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

    private bool shooting = false;

    private Transform player1;
    private Transform player2;

    private bool isPlayer1Close = false;
    private bool isPlayer2Close = false;

    public GameObject redLaserPrefab;
    public GameObject blueLaserPrefab;

    public Rigidbody redRB;
    public Rigidbody blueRB;

    private GameObject redLaser;
    private GameObject blueLaser;

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

            Invoke( "spawnEnemy", 0.5f);

            Invoke( "lookAtEnemy", 0.5f);

            Invoke( "shootAtEnemy", 1.2f);

            Invoke("removeEnemy", 2.5f);

            Invoke( "playersCanMove", 3.5f);
        }

    }

    private void FixedUpdate()
    {
        if (shooting)
        {
            Vector3 enemyPosition = new Vector3(nebula_pos.x, 0.0f, nebula_pos.z);
            
            Vector3 redTargetPosition = enemyPosition - redLaser.transform.position;
            redLaser.GetComponent<Rigidbody>().AddForce( redTargetPosition * 55 * Time.fixedDeltaTime );

            Vector3 blueTargetPosition = enemyPosition - blueLaser.transform.position;
            blueLaser.GetComponent<Rigidbody>().AddForce( blueTargetPosition * 55 * Time.fixedDeltaTime );
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
        // player1 stop moving
        GameObject p1 = GameObject.FindGameObjectsWithTag(tagPlayer1)[0];
        fc1 = p1.GetComponent<FiducialController>();
        fc1.fighting = true;

        // player2 stop moving
        GameObject p2 = GameObject.FindGameObjectsWithTag(tagPlayer2)[0];
        fc2 = p2.GetComponent<FiducialController>();
        fc2.fighting = true;

        // target is enemy, look at it
        Vector3 enemyPosition = new Vector3(nebula_pos.x, 0.0f, nebula_pos.z);

        fc1.transform.LookAt(enemyPosition, new Vector3(0, 1, 0));
        fc2.transform.LookAt(enemyPosition, new Vector3(0, 1, 0));

    }

    private void shootAtEnemy()
    {
        float red_laser_x = player1.position.x + 0.2f * (nebula_pos.x - player1.position.x);
        float red_laser_z = player1.position.z + 0.2f * (nebula_pos.z - player1.position.z);

        float blue_laser_x = player2.position.x + 0.2f * (nebula_pos.x - player2.position.x);
        float blue_laser_z = player2.position.z + 0.2f * (nebula_pos.x - player2.position.z);

        // RedLaser ----------------------------------------------------------------------------------
        Vector3 redLaserPosition = new Vector3(red_laser_x, 7.0f, red_laser_z); // parent cannot have y axis position, only child
        
        Transform red_trans= redLaserPrefab.transform;
        red_trans.rotation = player1.transform.rotation;
        red_trans.transform.Rotate(new Vector3(1,0,0), 90f);

        redLaser = Instantiate(redLaserPrefab, redLaserPosition, red_trans.transform.rotation);


        // BlueLaser ---------------------------------------------------------------------------------

        Vector3 blueLaserPosition = new Vector3(blue_laser_x, 7.0f, blue_laser_z); // parent cannot have y axis position, only child
        
        Transform blue_trans = blueLaserPrefab.transform;
        blue_trans.rotation = player2.transform.rotation;
        blue_trans.transform.Rotate(new Vector3(1, 0, 0), 90f);
        
        blueLaser = Instantiate(blueLaserPrefab, blueLaserPosition, blue_trans.transform.rotation);


        shooting = true;
        SoundManager.Instance.PlayShootClip();
    }

    private void removeEnemy()
    {
        // los disparos avanzan y paran al llegar a nebula_pos y luego hacemos animación de explosión
        Destroy(enemy);
        enemyList.Remove(enemy);
        SoundManager.Instance.PlayEnemyHitClip();

        Destroy(redLaser);
        Destroy(blueLaser);


        shooting = false;
    }

    private void playersCanMove()
    {
        // both players can move again
        fc1.fighting = false;
        fc2.fighting = false;
    }
}
