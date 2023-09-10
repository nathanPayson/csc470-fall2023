using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInitialMovement : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject collisionStopper;
    Boolean notCollided = true;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(new Vector3(0,0,-4500));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        Debug.Log("With:" + collision.gameObject.tag);
        if(collision.gameObject.tag == "Pin")
        {
            notCollided = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && notCollided)
        {
            transform.position = transform.position + new Vector3(-10 * Time.deltaTime, 0, 0);
        }
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && notCollided)
        { 
            transform.position = transform.position + new Vector3(10 * Time.deltaTime, 0, 0);
        }
    }
}
