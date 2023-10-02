using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    protected float cooldownLength;
    protected float cooldown;
    
    public Ability()
    {

    }
    public Ability(float cooldownLength)
    {
        this.cooldownLength = cooldownLength;
        this.cooldown = 0;

    }

    public void Update()
    {
        if (cooldown < 0)
        {
            cooldown = 0;
        }
        if (cooldown > 0) {
            this.cooldown -= Time.deltaTime;
        }
    }

    public bool activate(GameObject target)
    {
        bool worked = false;
        if (cooldown <= 0)
        {
            //doAbility(target);
            worked = true;
            cooldown = this.cooldownLength;
        }
        return worked;
    }

    void doAbility(GameObject target)
    {

    }
    public float getCooldown()
    {
        return cooldown;
    }


}
