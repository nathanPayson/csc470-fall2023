using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.forward * 1000); 
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Transform t = gameObject.GetComponent<Transform>();
            Vector3 timeToMove = transform.forward * 5;
            transform.position = transform.position + timeToMove*Time.deltaTime;
        }
    }
}
