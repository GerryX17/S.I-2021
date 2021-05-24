using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FiducialController fc;
    public int id;

    public float speed;
    public float rotationSpeed;

    enum players {RED_PLAYER = 0, BLUE_PLAYER = 2}

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        bool isRedPlayerMoving = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        bool isBluePlayerMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        int RedPlayer = (int)players.RED_PLAYER;
        int BluePlayer = (int)players.BLUE_PLAYER;

        bool isRedPlayer = fc.MarkerID == RedPlayer;
        bool isBluePlayer = fc.MarkerID == BluePlayer;

        if ( (isRedPlayerMoving && isRedPlayer) || (isBluePlayerMoving && isBluePlayer) ) // only for red or blue player
        {
            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);


            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

        }

        
    }

}
