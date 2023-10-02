using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability3 : Ability
{
    public Ability3() { }
    public Ability3(float cooldownLength)
    {
        this.cooldownLength = cooldownLength;
    }

    public void doAbility(GameObject target)
    {
        target.GetComponentInChildren<Health>().gainHealth(5);
    }
}
