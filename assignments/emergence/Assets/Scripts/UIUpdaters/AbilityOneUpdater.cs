using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityOneUpdater : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;

    float cooldown;
    float gcd;

    // Update is called once per frame
    void Update()
    {
        cooldown = player.GetComponent<PlayerController>().getAbility1().getCooldown();
        gcd = player.GetComponent<PlayerController>().getGCD();
        if (cooldown > gcd)
        {
            text.SetText("Shock blast: " + ((int)(cooldown * 10)) / 10.0);
        }
        else{
            text.SetText("Shock blast: " + ((int)(gcd * 10)) / 10.0);
        }
    }
}
