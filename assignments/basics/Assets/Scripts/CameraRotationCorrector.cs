using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationCorrector : MonoBehaviour
{
    public GameObject camera;
    public GameObject ball;
    Vector3 offest = new Vector3(0, 10, 30);
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
        camera.transform.rotation = Quaternion.Euler(10, 180, 0);
        camera.transform.position = ball.GetComponentInParent<Transform>().position + offest;
    }
}
