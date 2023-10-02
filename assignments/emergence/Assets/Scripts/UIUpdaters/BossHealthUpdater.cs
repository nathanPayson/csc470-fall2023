using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealthUpdater : MonoBehaviour
{
    public GameObject boss;
    public TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        text.SetText("Boss Health: " + boss.GetComponent<Health>().getHealth());
    }
}
