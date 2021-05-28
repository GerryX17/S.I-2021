using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    public Vector3 rotation;

    private float x_rotate;
    private float y_rotate;
    private float z_rotate;

    // Start is called before the first frame update
    void Start()
    {
        float x_rot = Random.Range(-45, 45);
        float y_rot = Random.Range(-45, 45);
        float z_rot = Random.Range(-45, 45);

        transform.Rotate(new Vector3(x_rot, y_rot, z_rot));


        x_rotate = Random.Range(-1.5f, 1.5f);
        y_rotate = Random.Range(-1.5f, 1.5f);
        z_rotate = Random.Range(-1.5f, 1.5f);

        rotation = new Vector3(x_rotate, y_rotate, z_rotate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation);
    }
}
