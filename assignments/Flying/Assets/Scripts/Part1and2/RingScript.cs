using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    bool through = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (!through && collision.gameObject.tag == "Player")
        {
            through = true;
            GameManager.SharedInstance.updateRings();
        }
        //crazyEffect();
    }
}
