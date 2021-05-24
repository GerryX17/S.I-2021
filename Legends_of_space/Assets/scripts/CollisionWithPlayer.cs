using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithPlayer : MonoBehaviour
{

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.rigidbody.CompareTag("Player1") || collision.rigidbody.CompareTag("Player1") )
        {
            Destroy(collision.rigidbody);
        }
    }
}
