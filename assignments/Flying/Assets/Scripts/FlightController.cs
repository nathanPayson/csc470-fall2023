using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    //Raycast --> GameObject --> What's the terrain --> Sample Height --> Pass your position
    //OnCollision Enter
    public GameObject flier;
    public GameObject camera;
    float flightScalar = 200;
    int grav = 1;
    float[] flightspeed = { 0, 0, 1 };
    float[] rot = { 0, 0, 0 }; //x,y,z
    int[] rotSpeed = {60,90,60};
    void Update()
    {
        //Input Management
        float hMove = Input.GetAxis("Horizontal");
        float vMove = -Input.GetAxis("Vertical");
//rotation Code
        //Rotation Management
        rot[0] += rotSpeed[0]* Time.deltaTime * vMove;
        rot[1] += rotSpeed[1]* Time.deltaTime * hMove;
        rot[2] -= rotSpeed[2] * Time.deltaTime * hMove;

        //Restabilization
        //Max Rotation in Z-Rot is +/- 30
        if (rot[2] > 50) rot[2] = 50;
        if (rot[2] < -50) rot[2] = -50;
        //If not turning on y-rot then z-rot moves back towards 0
        if (hMove == 0 && rot[2] > 1) rot[2] -= 50*Time.deltaTime;
        else if (hMove == 0 && rot[2] < -1) rot[2] += 50*Time.deltaTime;


        //flier Rotation
        Transform transform = flier.transform;
        transform.eulerAngles = new Vector3(rot[0], rot[1], rot[2]);

        //Collision Check?




        //Movement Code
        //flier Movement
        transform.position += transform.forward * flightScalar * Time.deltaTime;
        camera.transform.position = transform.position - transform.forward * 20 + new Vector3(0, 5, 0);
        camera.transform.LookAt(transform.position);



    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("ping");
        if (collision.gameObject.tag == "Untagged")
        {
            UnityEngine.Object.Destroy(gameObject);
            GameManager.SharedInstance.playerDied();
        }
    }
}
