using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    int hitPoints = 8;

    public void setHealth(int amount)
    {
        hitPoints = amount;
    }
    public bool takeDamage(int amount)
    {
        hitPoints -= amount;
        if(gameObject.tag == "Boss")
        {
            GameManager.SharedInstance.setBossHealth(hitPoints);
        }

        if (hitPoints <= 0)
        {
            return true;
        }
        return false;
    }

    public int getHealth()
    {
        return hitPoints;
    }
}
