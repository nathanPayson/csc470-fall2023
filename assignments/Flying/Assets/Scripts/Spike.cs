using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    GameObject kill;

    public void Update()
    {
        if(kill != null)
        {
            kill.GetComponent<PlayerController>().kill();
            kill = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerPlat")
        {
            Debug.Log("Ping");
            kill = collision.gameObject;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerPlat")
        {
            Debug.Log("Ping");
            kill = collision.gameObject;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerPlat")
        {
            Debug.Log("Ping");
            kill = collision.gameObject;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerPlat")
        {
            Debug.Log("Ping");
            kill = collision.gameObject;
        }
    }
}
