using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public Transform t;
    public Rigidbody rb;

    public float speed;
    public float timeToDestroy;

    public float percentScaled;
    public float startScale;
    public float targetScale;

    private Vector3 player_forward;

    bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroyed) {


            if (percentScaled < 1f)
            {
                rb.AddRelativeForce(player_forward * speed * Time.deltaTime);

                percentScaled += Time.deltaTime / timeToDestroy;
                float scale = Mathf.Lerp(startScale, targetScale, percentScaled);
                transform.localScale = new Vector3(scale, scale, scale);

                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Player1") || other.CompareTag("Player2") )
        {

            SoundManager.Instance.PlayAsteroidHitClip();

            player_forward = other.transform.forward;

            Destroy(this.gameObject, timeToDestroy);
            isDestroyed = true;
        }
    }

}
