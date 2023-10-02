using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthAmount;
    bool alive = true;
    int maxHealth;

    void Start()
    {
        maxHealth = healthAmount; 
    }
    public void loseHealth(int damage)
    {
        healthAmount -= damage;
        if (healthAmount <= 0)
        {
            alive = false;
        }
        if (!alive)
        {
            healthAmount = 0;
        }
    }

    private void Update()
    {
        if (healthAmount <= 0)
        {
            alive = false;
        }
    }
    public int getHealth()
    {
        return healthAmount;
    }

    public void gainHealth(int amount)
    {
        if (healthAmount<maxHealth)
        {
            healthAmount += amount;
        }

    }
    public bool checkAlive()
    {
        return alive;
    }
}
