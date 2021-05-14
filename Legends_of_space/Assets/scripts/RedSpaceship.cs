using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSpaceship : MonoBehaviour
{
    public FiducialController fc;
    public float angle;

    private Vector3 last_position;
    private Vector3 actual_position;
    private Vector3 change_position;

    // Start is called before the first frame update
    void Start()
    {
        last_position = fc.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        actual_position = fc.transform.position;
        change_position = actual_position - last_position;

        //Debug.Log(change_position);
        if (change_position.x > 0)
        {
            fc.transform.Rotate(0, angle * Time.deltaTime, 0);
        }
        if (change_position.x < 0)
        {
            fc.transform.Rotate(0, -angle * Time.deltaTime, 0);
        }

        float y_rotation = fc.transform.eulerAngles.y;

        if (change_position.z > 0 && y_rotation > 0)
        {
            fc.transform.Rotate(0, -( y_rotation - (0.15f * y_rotation) )  * Time.deltaTime, 0);
        }
        if (change_position.z > 0 && y_rotation < 0)
        {
            fc.transform.Rotate(0, (y_rotation - (0.15f * y_rotation)) * Time.deltaTime, 0);
        }

        if (change_position.z < 0 && y_rotation > 0)
        {
            fc.transform.Rotate(0, (y_rotation - (0.15f * y_rotation)) * Time.deltaTime, 0);
        }
        if (change_position.z < 0 && y_rotation < 0)
        {
            fc.transform.Rotate(0, -(y_rotation - (0.15f * y_rotation)) * Time.deltaTime, 0);
        }

        last_position = actual_position;
    }
}
