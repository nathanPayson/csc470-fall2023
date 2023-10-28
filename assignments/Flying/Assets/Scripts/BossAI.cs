using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    float timer;
    int scaler = 103;
    int previousPick = -1;

    void Start()
    {
        gameObject.transform.Rotate(new Vector3(0, -90, 0));
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer > 1.2 ) {
            int picker = -1;
            while (picker == -1 || picker == previousPick)
            {
                picker = Random.Range(0, 6);
            }
            previousPick = picker;
            if(picker == 0)
            {
                upAttack();
                Debug.Log("UP ATTACK");
            }
            if (picker == 1)
            {
                middleAttack();
                Debug.Log("MIDDLE ATTACK");
            }
            if (picker == 2)
            {
                downAttack();
                Debug.Log("DOWN ATTACK");
            }
            if (picker == 3)
            {
                Debug.Log("RIGHT ATTACK");
                rightAttack();
            }
            if (picker == 4)
            {
                Debug.Log("LEFT ATTACK");
                leftAttack();
            }
            if (picker == 5)
            {
                //Break From dodging
            }
            if (picker == 6)
            {
                Debug.Log("BEAM ATTACK");
                beamAttack();
            }
            timer = Time.time;
        }
        //Attack Up (Top 400 Y)

        //Attack Middle (Middle 400 Z)

        //Attack Down (Bottom 400 Y)

        //Beam (Target last position (10 ago)

        //Kill Move
        Transform transform = gameObject.transform;
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x + 350, transform.position.y, transform.position.z);
        }
    }

    void upAttack()
    {
        for (int i = -3; i < 3; i++)
        {
            for (int j = 2; j < 6; j++)
            {
                GameObject miniProjectile = Instantiate(projectile, transform.position + new Vector3(-30, j * scaler, i * scaler), Quaternion.identity);
                Object.Destroy(miniProjectile, 5f);
            }
        }
    }

    void middleAttack()
    {
        for (int i = -1; i < 1; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject miniProjectile = Instantiate(projectile, transform.position + new Vector3(-30, j * scaler, -i * scaler), Quaternion.identity);;
                Object.Destroy(miniProjectile, 5f);
            }
        }
    }

    void downAttack()
    {
        for (int i = -3; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject miniProjectile = Instantiate(projectile, transform.position + new Vector3(-30, j * scaler, i * scaler), Quaternion.identity);
                Object.Destroy(miniProjectile, 5f);
            }
        }
    }
    void rightAttack()
    {
        for (int i = -30; i < 0; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                GameObject miniProjectile = Instantiate(projectile, transform.position + new Vector3(-30, j * scaler, i * scaler), Quaternion.identity);
                Object.Destroy(miniProjectile, 5f);
            }
        }
    }
    void leftAttack()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                GameObject miniProjectile = Instantiate(projectile, transform.position + new Vector3(-30, j * scaler, i * scaler), Quaternion.identity);
                Object.Destroy(miniProjectile, 5f);
            }
        }
    }

    void beamAttack()
    {

    }
}
