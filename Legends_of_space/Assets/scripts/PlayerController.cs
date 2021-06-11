using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;

    public float threshold;

    private Vector3 last_position;
    private Vector3 current_position;

    private void Start()
    {
        last_position = transform.position;
        current_position = transform.position;
    }

    void Update()
    {
        float horizontalInput = 0f, verticalInput = 0f;
        current_position = transform.position;


        if (current_position.z != last_position.z)
        {
            verticalInput = current_position.z - last_position.z;
            last_position = current_position;
        }

        if (current_position.x != last_position.x)
        {
            horizontalInput = current_position.x - last_position.x;
            last_position = current_position;
        }

        Vector3 movementDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        Vector3 finalDirection = Vector3.zero;

        float x_offset_absolute, z_offset_absolute;

        x_offset_absolute = Mathf.Abs(movementDirection.x);
        z_offset_absolute = Mathf.Abs(movementDirection.z);

        if (x_offset_absolute  > threshold && z_offset_absolute > threshold)
            finalDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        else if (x_offset_absolute > threshold)
            finalDirection = new Vector3(horizontalInput, 0.0f, 0.0f);
        else if (z_offset_absolute > threshold)
            finalDirection = new Vector3(0.0f, 0.0f, verticalInput);

        if (finalDirection != Vector3.zero)
        {
            if (finalDirection.x > 0 && finalDirection.z > 0)
            {
                Debug.Log("Usando tu quaternion bobo");
                //Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                Quaternion toRotation = Quaternion.LookRotation(Quaternion.Euler(0, 45, 0) * movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

                //Quaternion.Euler(0, 45, 0) * movementDirection
            }
            else
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

}