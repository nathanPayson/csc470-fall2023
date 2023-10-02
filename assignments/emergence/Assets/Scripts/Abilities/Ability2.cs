using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability2 : Ability
{
    // Start is called before the first frame update
    public Ability2() { }
    public Ability2(float cooldownLength)
    {
        this.cooldownLength = cooldownLength;
    }

    public void doAbility(GameObject target)
    {
        target.GetComponentInChildren<Health>().loseHealth(10);
    }
}
