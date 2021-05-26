using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;

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


        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        //movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

}