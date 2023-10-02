using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ability1 : Ability
{
    // Start is called before the first frame update
   public Ability1(){}
   public Ability1(float cooldownLength)
    {
        this.cooldownLength = cooldownLength;
    }

    public void doAbility(GameObject target)
    {
        Debug.Log("DoingAbility");
        target.GetComponentInChildren<Health>().loseHealth(1);
    }
}
