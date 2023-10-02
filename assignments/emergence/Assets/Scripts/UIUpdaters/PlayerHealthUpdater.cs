using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthUpdater : MonoBehaviour
{
    public GameObject player;
    public TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        text.SetText("Player Health: " + player.GetComponent<Health>().getHealth());
    }
}
