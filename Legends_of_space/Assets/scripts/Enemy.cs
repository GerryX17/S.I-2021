using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runSpeed;
    public float gotShotDestroyDelay;
    private bool hitByShot;

    public float dropDestroyDelay; // 1
    private Collider myCollider; // 2
    private Rigidbody myRigidbody;
    private EnemySpawner enemySpawner;
    public float heartOffset; // 1
    public GameObject heartPrefab; // 2

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);

    }

    private void Drop()
    {
        enemySpawner.RemoveEnemyFromList(gameObject, hitByShot);
        myRigidbody.isKinematic = false; // 1
        myCollider.isTrigger = false; // 2
        Destroy(gameObject, dropDestroyDelay); // 3
        SoundManager.Instance.PlayShipDroppedClip();

    }


    private void HitByHay()
    {
        hitByShot = true; // 1
        enemySpawner.RemoveEnemyFromList(gameObject, hitByShot);
        runSpeed = 0; // 2
        Destroy(gameObject, gotShotDestroyDelay); // 3

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        //TweenScale tweenScale = gameObject.AddComponent<TweenScale>(); ; // 1
        //tweenScale.targetScale = 0; // 2
        //tweenScale.timeToReachTarget = gotShotDestroyDelay; // 3
        SoundManager.Instance.PlayShipHitClip();

    }
    private void OnTriggerEnter(Collider other) // 1
    {
        if (other.CompareTag("Laser") && !hitByShot) // 2
        {
            Destroy(other.gameObject); // 3
            HitByHay(); // 4
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }

    }

    public void SetSpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }
}