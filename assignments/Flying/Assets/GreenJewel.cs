using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenJewel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.SharedInstance.foundJewel();
    }
}
