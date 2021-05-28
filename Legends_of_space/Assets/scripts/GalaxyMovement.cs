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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((x >= 0.0f && x <= 100.0f) && (z >= 0.0f && z <= 100.0f))
        {
            x = t.position.x;
            y = t.position.y;
            z = t.position.z;

            random = Random.Range(0.0f, 1.0f);

            t.Translate(speed, 0.0f, speed, Space.World);
        }
        else
        {
           
        }
        

        t.Rotate(0, 0.5f, 0);
    }
}
