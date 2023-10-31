using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    //Raycast --> GameObject --> What's the terrain --> Sample Height --> Pass your position
    //OnCollision Enter
    public GameObject flier;
    public GameObject camera;
    float flightScalar = 100;
    int grav = 1;
    float[] flightspeed = { 0, 0, 1 };
    float[] rot = { 0, 0, 0 }; //x,y,z
    int[] rotSpeed = {0,0,0};
    int difficultyDecided = -1;
    void Update()
    {
        if (difficultyDecided == -1)
        {
            difficultyDecided = GameManager.SharedInstance.getDifficulty();
            if(difficultyDecided == 1)
            {
                flightScalar = 100;
                rotSpeed[0] = 30;
                rotSpeed[1] = 45;
                rotSpeed[2] = 30;
                GameManager.SharedInstance.setTimer(100);
                GameManager.SharedInstance.initiateScene1();
            }
            if (difficultyDecided == 2)
            {
                flightScalar = 150;
                rotSpeed[0] = 45;
                rotSpeed[1] = 60;
                rotSpeed[2] = 45;
                GameManager.SharedInstance.setTimer(70);
                GameManager.SharedInstance.initiateScene1();
            }
            if (difficultyDecided == 3)
            {
                flightScalar = 200;
                rotSpeed[0] = 60;
                rotSpeed[1] = 90;
                rotSpeed[2] = 60;
                GameManager.SharedInstance.setTimer(50);
                GameManager.SharedInstance.initiateScene1();
            }
        }
        else
        {
            //Input Management
            float hMove = Input.GetAxis("Horizontal");
            float vMove = -Input.GetAxis("Vertical");
            //rotation Code
            //Rotation Management
            rot[0] += rotSpeed[0] * Time.deltaTime * vMove;
            rot[1] += rotSpeed[1] * Time.deltaTime * hMove;
            rot[2] -= rotSpeed[2] * Time.deltaTime * hMove;

            //Restabilization
            //Max Rotation in Z-Rot is +/- 30
            if (rot[2] > 50) rot[2] = 50;
            if (rot[2] < -50) rot[2] = -50;
            //If not turning on y-rot then z-rot moves back towards 0
            if (hMove == 0 && rot[2] > 1) rot[2] -= 50 * Time.deltaTime;
            else if (hMove == 0 && rot[2] < -1) rot[2] += 50 * Time.deltaTime;


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
