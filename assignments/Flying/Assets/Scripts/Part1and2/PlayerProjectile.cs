using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Untagged")
        {
            Object.Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            bool dead = collision.gameObject.GetComponent<EnemyAI>().takeDamage(1);
            if (dead) Object.Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Enemy Bullet")
        {
            Object.Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Boss")
        {
            bool dead = collision.gameObject.GetComponent<EnemyAI>().takeDamage(1);
            if (dead) Object.Destroy(collision.gameObject);
        }
        Object.Destroy(gameObject);
    }
}
