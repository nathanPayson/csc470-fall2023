using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            bool dead = collision.gameObject.GetComponent<FlightController2>().takeDamage(1);
            //Deal Damage
            Object.Destroy(gameObject);
            if (dead)
            {
                Object.Destroy(collision.gameObject);
            }
        }
        if(collision.gameObject.tag == "Untagged")
        {
            Object.Destroy(gameObject);
        }
        
    }
}
