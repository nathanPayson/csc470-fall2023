using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicGhostAI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject projectile;
    public GameObject player;
    float timeSinceAttack;

    void Start()
    {
        timeSinceAttack= Time.time;
        gameObject.transform.Rotate(new Vector3(0, 180, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = gameObject.transform;
        //Every 10 Seconds - Launch ball at Player
        if(Time.time - timeSinceAttack > 8)
        {
            for(int i=0;i<10; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    GameObject miniProjectile = Instantiate(projectile, transform.position + new Vector3(i * 6, j*6, -30), Quaternion.identity);
                    Object.Destroy(miniProjectile, 10f);
                }
            }
            timeSinceAttack = Time.time;

            //Rotation Corrector
            

        }

        //If health is 0: Die

        //Move so that you are always 75 away from player in z direction and the same height

        if (player != null)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + 300);
        }


    }

    public void resetTimer()
    {
        timeSinceAttack = Time.time;
    }
}
