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
        t.Rotate(0, 0.5f, 0);
    }
}
