using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnGravityOn : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigidbody;
    void Start()
    {
        Debug.Log("Hello World!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.useGravity = true;
            Debug.Log("Hello World 2!");
        }
    }
}
