using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInitialMovement : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(new Vector3(0,0,-500));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
