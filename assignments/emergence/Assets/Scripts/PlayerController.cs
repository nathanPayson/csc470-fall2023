using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore;

public class PlayerController : MonoBehaviour
{
    float cooldown1 = 1;
    float cooldown2 = 10;
    float cooldown3 = 5;
    float gcd = 1;
    float speed = 10;
    float cameraSpeed = 90;
    float jumpCooldown = 1;
    Ability1 a1;
    Ability2 a2;
    Ability3 a3;
    public GameObject boss;
    int iFrames = 10;
    float iFramecounter = 0.5f;
    public GameObject player;
    float iFrameDerenderer = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        this.a1 = new Ability1(cooldown1);
        this.a2 = new Ability2(cooldown2);
        this.a3 = new Ability3(cooldown3);

    }

    // Update is called once per frame
    void Update()
    {
        Transform pos = player.transform;
        a1.Update();
        a2.Update();
        a3.Update();

        if (iFramecounter > 0)
        {
            iFramecounter -= Time.deltaTime;
        }
        if (iFramecounter <= 0)
        {
            immunityUpdate();
            iFramecounter = 0.5f;
        }
        if (immune())
        {
            iFrameDerenderer -= Time.deltaTime;
        }
        if(immune() && iFrameDerenderer < 0.1)
        {
            player.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
        if (immune() && iFrameDerenderer <= 0 || !immune())
        {
            player.GetComponentInChildren<MeshRenderer>().enabled = true;
            iFrameDerenderer = 0.2f;
        }
        if (gcd < 0)
        {
            gcd = 0;
        }
        else if (gcd > 0)
        {
            gcd -= Time.deltaTime;
        }
        if (jumpCooldown < 0)
        {
            jumpCooldown = 0;
        }
        else if (jumpCooldown> 0)
        {
            jumpCooldown -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            pos.Rotate(0, cameraSpeed * Time.deltaTime, 0);
            //gameObject.pos( speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            //Move backwards by: speed* Time.deltaTime
            pos.position -= pos.forward * speed * Time.deltaTime;

        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            pos.Rotate(0, -cameraSpeed * Time.deltaTime,0);

            // speed*Time.deltaTime

        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            pos.position += pos.forward * speed * Time.deltaTime;
            
            //Move forward by speed*Time.deltaTime
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && (gcd <= 0))
{
            bool worked = a1.activate(boss);

            if(worked) 
            {
                a1.doAbility(boss);
                gcd = 1; 
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && (gcd <= 0))
        {
            bool worked = a2.activate(boss);

            if (worked)
            {
                a2.doAbility(boss);
                gcd = 1;
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && (gcd <= 0))
        {
            bool worked = a3.activate(gameObject);

            if (worked)
            {
                a3.doAbility(gameObject);
                gcd = 1;
            }

        }
        if (Input.GetKeyDown(KeyCode.Space) && (jumpCooldown <= 0)){
            //Jump
            jumpCooldown = 1;
            player.GetComponent<Rigidbody>().AddForce(0, 400, 0);
        }
    }
    public Ability1 getAbility1()
    {
        return a1;
    }
    public Ability2 getAbility2()
    {
        return a2;
    }
    public Ability3 getAbility3()
    {
        return a3;
    }
    public float getGCD()
    {
        return gcd;
    }
    public void OnCollisionStay(Collision collision)
    {

        if(collision.gameObject.tag == "Off Cell" && !immune())
        {
            player.GetComponent<Health>().loseHealth(10);
            setImmunity(10);
        }
    }
    public void setImmunity(int iFrames)
    {
        this.iFrames = iFrames;
    }

    public bool immune()
    {
        if (iFrames > 0)
        {
            return true;
        }
        return false;
    }
    public void immunityUpdate()
    {
        if (iFrames > 0)
        {
            iFrames -= 1;
        }
    }
}
