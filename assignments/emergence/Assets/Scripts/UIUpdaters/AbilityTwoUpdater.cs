using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityTwoUpdater : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;

    float cooldown;
    float gcd;


    // Update is called once per frame
    void Update()
    {
        cooldown = player.GetComponent<PlayerController>().getAbility2().getCooldown();
        gcd = player.GetComponent<PlayerController>().getGCD();
        if (cooldown > gcd)
        {
            text.SetText("Mega Blast: " + ((int)(cooldown * 10)) / 10.0);
        }
        else
        {
            text.SetText("Mega Blast: " + ((int)(gcd * 10)) / 10.0);
        }
    }
}
