using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GalaxyMovement : MonoBehaviour
{
    public Transform t;
    private Rigidbody rigidbody;

    public float initialSpeed;

    private Vector3 initialMovementDirection;

    public float speed;

    private int numBounces;
    private Vector3 reflection;


    // Start is called before the first frame update
    void Start()
    {
        initialMovementDirection = new Vector3(8f, 0f, 8f);

        numBounces = 1;

        rigidbody = t.GetComponent<Rigidbody>();

        // first movement until collision
        rigidbody.velocity += initialMovementDirection * Time.deltaTime * initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        t.Rotate(0, 0.5f, 0);

        
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact;
        if (numBounces > 0) // if has reached max number of bounces
        {
            contact = collision.contacts[0]; // get first contact on collision
            float dot = Vector3.Dot(contact.normal, (-transform.forward)); // get the dot product vector
            dot *= 2; // square it up
            reflection = contact.normal * dot; // reflection is normal scaled with the dot product and the forward
            reflection = reflection + transform.forward;
            rigidbody.velocity = Vector3.MoveTowards(t.position * Time.deltaTime * speed, reflection * Time.deltaTime * speed, 0.05f); 
            numBounces -= 1;
        }
    }
}
