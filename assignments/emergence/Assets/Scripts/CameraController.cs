using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position - (player.transform.forward * 10) + new Vector3(0, 5, 0);
        gameObject.transform.rotation = player.transform.rotation;

    }
}
