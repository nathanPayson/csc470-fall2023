using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenManager : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    public TextMeshProUGUI text;
    bool winnerDeclared = false;
    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<Health>().checkAlive() && !winnerDeclared)
        {
            text.SetText("You Lose...");
            winnerDeclared = true;
        }
        else if (!boss.GetComponent<Health>().checkAlive() && !winnerDeclared)
        {
            text.SetText("You win!!!!");
            winnerDeclared = true;
        }
        else if (!winnerDeclared)
        {
            text.SetText("");
        }
    }
}
