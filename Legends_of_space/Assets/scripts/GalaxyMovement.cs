using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GalaxyMovement : MonoBehaviour
{
    private FiducialController fc1;
    private FiducialController fc2;

    public Transform t;
    private Rigidbody rigidbody;

    public float initialSpeed;

    private Vector3 initialMovementDirection;

    public float speed;
    public string tagPlayer1, tagPlayer2;
    public GameObject Tp_Prefab;
    public Material gold, steel;

    private Transform galaxy;
    private Vector3 galaxy_pos;
    private Transform player1;
    private Transform player2;
    private bool isPlayer1Close = false;
    private bool isPlayer2Close = false;
    private bool hasSwapped = false;
    private List<GameObject> tp_list = new List<GameObject>();

    private int numBounces;
    private Vector3 reflection;

    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        initialMovementDirection = new Vector3(8f, 0f, 8f);

        numBounces = 1;

        rigidbody = t.GetComponent<Rigidbody>();
     
        galaxy = GetComponent<Transform>();

        GameObject p1 = GameObject.FindGameObjectsWithTag(tagPlayer1)[0];
        player1 = p1.transform;

        GameObject p2 = GameObject.FindGameObjectsWithTag(tagPlayer2)[0];
        player2 = p2.transform;

        // first movement until collision
        rigidbody.velocity += initialMovementDirection * Time.deltaTime * initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        t.Rotate(0, 0.5f, 0);
        galaxy_pos = galaxy.position;
        float dist1 = Vector3.Distance(player1.position, galaxy_pos);
        if (dist1 < 20)
            isPlayer1Close = true;
        else
            isPlayer1Close = false;

        float dist2 = Vector3.Distance(player2.position, galaxy_pos);
        if (dist2 < 20)
            isPlayer2Close = true;
        else
            isPlayer2Close = false;


        if ((isPlayer1Close && isPlayer2Close) && !hasSwapped){

            hasSwapped = true;
            Invoke("spawnTP",1f);
            Invoke("materialSwapper", 3f);
            Invoke("playersCanMove", 4f);

        }



    }

    private void spawnTP()
    {
        // player1 stop moving
        GameObject p1 = GameObject.FindGameObjectsWithTag(tagPlayer1)[0];
        fc1 = p1.GetComponent<FiducialController>();
        fc1.fighting = true;

        // player2 stop moving
        GameObject p2 = GameObject.FindGameObjectsWithTag(tagPlayer2)[0];
        fc2 = p2.GetComponent<FiducialController>();
        fc2.fighting = true;

        Vector3 position1 = new Vector3(player1.position.x, player1.position.y, player1.position.z);
        Vector3 position2 = new Vector3(player2.position.x, player2.position.y, player2.position.z);
        GameObject tp1 = new GameObject();
        tp1 = Instantiate(Tp_Prefab, position1, player1.rotation);
        tp_list.Add(tp1);
        GameObject tp2 = new GameObject();
        tp2 = Instantiate(Tp_Prefab, position2, player2.rotation);
        tp_list.Add(tp2);
        SoundManager.Instance.PlayTransformClip();

    }

    private void materialSwapper()
    {
        Destroy(tp_list[0]);
        tp_list.Remove(tp_list[0]);
        Destroy(tp_list[0]);
        tp_list.Remove(tp_list[0]);
        // Get the current material applied on the GameObject
        GameObject p1 = GameObject.FindGameObjectWithTag(tagPlayer1).transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
        //Debug.Log(p1.tag);
        var mat1 = p1.GetComponent<MeshRenderer>().materials;
        mat1[0] = steel;
        //Debug.Log(mat1[0]);
        p1.GetComponent<MeshRenderer>().materials = mat1;
        GameObject p2 = GameObject.FindGameObjectWithTag(tagPlayer2).transform.GetChild(2).gameObject.transform.GetChild(1).gameObject;
        var mat2 = p2.GetComponent<MeshRenderer>().materials;
        Debug.Log(mat2[0]);
        mat2[0] = gold;
        Debug.Log(mat2[0]);
        p2.GetComponent<MeshRenderer>().materials = mat2;

    }

    private void playersCanMove()
    {
        // both players can move again
        fc1.fighting = false;
        fc2.fighting = false;
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
