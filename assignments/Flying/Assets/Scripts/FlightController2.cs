using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController2 : MonoBehaviour
{

    public GameObject flier;
    public GameObject camera;
    float flightScalar = 200;
    int grav = 1;
    float[] flightspeed = { 0, 0, 1 };
    float[] rot = { 0, 0, 0 }; //x,y,z
    int[] rotSpeed = { 30, 90, 30 };
    float globalTimer = 0;
    int hitPoints = 5;

    public GameObject projectile;
    float timeSinceDamage;

    private void Start()
    {
        globalTimer = Time.time;
        timeSinceDamage = Time.time;
        GameManager.SharedInstance.setPlayerHealth(5);

    }
    void Update()
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
        if (rot[2] > 30) rot[2] = 30;
        if (rot[2] < -30) rot[2] = -30;
        //If not turning on y-rot then z-rot moves back towards 0
        if (hMove == 0 && rot[2] > 1) rot[2] -= 30 * Time.deltaTime;
        else if (hMove == 0 && rot[2] < -1) rot[2] += 30 * Time.deltaTime;


        //flier Rotation
        Transform transform = flier.transform;
        transform.eulerAngles = new Vector3(rot[0], rot[1], rot[2]);


        //Movement Code
        //flier Movement

        if (Input.GetKey(KeyCode.Space) && (Time.time - globalTimer) > .5)
        {
            //Shoot Projectile

            GameObject miniProjectile = Instantiate(projectile, transform.position + transform.forward *30, Quaternion.identity);
            Rigidbody rb = miniProjectile.GetComponent<Rigidbody>();
            rb.AddForce(flier.transform.forward * 25000);
            globalTimer = Time.time;
        }
        transform.position += transform.forward * flightScalar * Time.deltaTime;
        if(transform.position.y >= 400)
        {
            transform.position = new Vector3(transform.position.x, 400, transform.position.z);
        }
        camera.transform.position = transform.position - transform.forward * 20 + new Vector3(0, 5, 0);
        camera.transform.LookAt(transform.position);
    }
    public void setHealth(int amount)
    {
        hitPoints = amount;
    }
    public bool takeDamage(int amount)
    {
        if(Time.time - timeSinceDamage > 0.5) {
            hitPoints -= amount;
            GameManager.SharedInstance.setPlayerHealth(hitPoints);
            Debug.Log("Taken: " + amount + " damage");
            timeSinceDamage = Time.time;
        }
        if (hitPoints <= 0)
        {
            return true;
        }
        return false;
    }

    public int getHealth()
    {
        return hitPoints;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("ping");
        if(collision.gameObject.tag == "Untagged")
        {
            UnityEngine.Object.Destroy(gameObject);
            GameManager.SharedInstance.playerDied();
        }
    }
}

