using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GalaxyMovement : MonoBehaviour
{
    public float random;

    public float x;
    public float y;
    public float z;

    public Transform t;

    public float speed;

    private Vector3 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t.Rotate(0, 0.5f, 0);
        transform.Translate(speed, 0.0f, speed / 2, Space.World);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("Wall"))
        {
            speed = lastVelocity.magnitude;
            //var direction = Vector3.Reflect(lastVelocity.normalized);
            // transform.velocity = direction * Math.(speed, 0f);
        }
    }
}
