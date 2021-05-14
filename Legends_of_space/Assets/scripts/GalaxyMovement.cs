using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GalaxyMovement : MonoBehaviour
{
    public float random;

    public float x;
    public float y;
    public float z;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         x = transform.position.x;
         y = transform.position.y;
         z = transform.position.z;

        random = Random.Range(0.0f, 1.0f);
        if (random > 0.4)
        {
            transform.Translate(speed, 0.0f, speed/2, Space.World);
        }
        else
        {
            transform.Translate(speed/2, 0.0f, speed, Space.World);
        }

        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        if ( x > 100.0f )
        {
            transform.Translate(-99.0f, 0.0f, -z/3);
        }
        else if ( x < 0.0f )
        {
            //transform.Translate(-(x-1), 0.0f, -z/3);

        }

        if (z > 100.0f)
        {
            transform.Translate(-x/3, 0.0f, -99.0f);
        }
        else if (z < 0.0f)
        {
            //transform.Translate(-x/3, 0.0f, -(z-1));

        }

        transform.Rotate(0, 0.5f, 0);
    }
}
