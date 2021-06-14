using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t.Rotate(new Vector3(0, -2f, 0));
    }
}
