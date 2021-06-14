using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaTrigger : MonoBehaviour
{
    public FiducialController fc1;
    public FiducialController fc2;

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

    public float timeBetweenSpawns;

    private bool arePlayersFighting = false;

    private List<GameObject> enemyList = new List<GameObject>();

    private EnemiesSpawner enemySpawner;

    private void Start()
    {
        nebula = GetComponent<Transform>();
        nebula_pos = nebula.position;

        GameObject p1 = GameObject.FindGameObjectsWithTag (tagPlayer1)[0];
        player1 = p1.transform;
        fc1 = p1.GetComponent<FiducialController>();

        GameObject p2 = GameObject.FindGameObjectsWithTag(tagPlayer2)[0];
        player2 = p2.transform;
        fc2 = p2.GetComponent<FiducialController>();

        enemySpawner = EnemiesSpawner.instance;

        StartCoroutine(fightRoutine());
    }

    private void Update()
    {
        // is player 1 within the range of spawning the enenmy or not
        float dist1 = Vector3.Distance(player1.position, nebula_pos);
        if (dist1 < 20)
            isPlayer1Close = true;
        else
            isPlayer1Close = false;

        // is player 2 within the range of spawning the enenmy or not
        float dist2 = Vector3.Distance(player2.position, nebula_pos);
        if (dist2 < 20)
            isPlayer2Close = true;
        else
            isPlayer2Close = false;

        // if the enemy has not spawned yet and both players are together then spawn the enemy
        if ( (isPlayer1Close && isPlayer2Close) )
        {
            arePlayersFighting = true;
        }
    }

    private IEnumerator fightRoutine()
    {
        while (true) 
        { 
            if (arePlayersFighting == true)
            {
                yield return new WaitForSeconds(0.2f);
                spawnEnemy();
                yield return new WaitForSeconds(0.2f);
                lookAtEnemy();
                yield return new WaitForSeconds(0.6f);
                shootAtEnemy();
                yield return new WaitForSeconds(1.2f);
                removeEnemy();
                yield return new WaitForSeconds(0.5f);
            }
        yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (shooting)
        {
            Vector3 enemyPosition = new Vector3(nebula_pos.x, 0.0f, nebula_pos.z);

            Vector3 redTargetPosition = enemyPosition - redLaser.transform.position;
            redLaser.GetComponent<Rigidbody>().AddForce(redTargetPosition * 55 * Time.fixedDeltaTime);

            Vector3 blueTargetPosition = enemyPosition - blueLaser.transform.position;
            blueLaser.GetComponent<Rigidbody>().AddForce(blueTargetPosition * 55 * Time.fixedDeltaTime);

        }
    }

    private void spawnEnemy()
    {
        enemySpawner.canEnemySpawn = true;
    }

    private void lookAtEnemy()
    {
        // player1 stop moving
        fc1.doNotMove();

        // player2 stop moving
        fc2.doNotMove();

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
        Vector3 redLaserPosition = new Vector3(red_laser_x, 11.0f, red_laser_z); // parent cannot have y axis position, only child
        
        Transform red_trans= redLaserPrefab.transform;
        red_trans.rotation = player1.transform.rotation;
        red_trans.transform.Rotate(new Vector3(1,0,0), 90f);

        redLaser = Instantiate(redLaserPrefab, redLaserPosition, red_trans.transform.rotation);


        // BlueLaser ---------------------------------------------------------------------------------

        Vector3 blueLaserPosition = new Vector3(blue_laser_x, 8.0f, blue_laser_z); // parent cannot have y axis position, only child
        
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
        enemySpawner.removeEnemy();

        SoundManager.Instance.PlayEnemyHitClip();

        Destroy(redLaser);
        Destroy(blueLaser);

        shooting = false;

        fc1.moveAgain();
        fc2.moveAgain();

        arePlayersFighting = false;
    }
}
