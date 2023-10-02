using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityThreeUpdater : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;

    float cooldown;
    float gcd;

    // Update is called once per frame
    void Update()
    {
        cooldown = player.GetComponent<PlayerController>().getAbility3().getCooldown();
        gcd = player.GetComponent<PlayerController>().getGCD();
        if (cooldown > gcd)
        {
            text.SetText("Repair: " + ((int)(cooldown * 10)) / 10.0);
        }
        else
        {
            text.SetText("Repair: " + ((int)(gcd * 10)) / 10.0);
        }
    }
}
