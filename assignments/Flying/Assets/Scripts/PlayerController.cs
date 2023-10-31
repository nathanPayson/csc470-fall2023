using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float rotationSpeed = 90;
    float movementSpeed = 4;
    float strafeSpeed = 3;
    float jumpForce = 1.25f;
    CharacterController cc;
    float yVelocity = -1;
    float gravityScalar = 0.3f;
    Vector3 respawnPoint;
    bool dead = false;

    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Input
        float vMove = Input.GetAxis("Vertical");
        float hMove = Input.GetAxis("Horizontal");
        float strafe = 0;
        bool jump = false;
        if(Input.GetKey(KeyCode.Q))
        {
            strafe = -1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            strafe = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.SharedInstance.forceLoss();
        }

        //Determine Rotation

        gameObject.transform.eulerAngles += new Vector3(0, hMove * rotationSpeed * Time.deltaTime);
        mainCamera.transform.eulerAngles += new Vector3(0, hMove * rotationSpeed * Time.deltaTime);

        //Determine Movement
        //Y Movement
        if (!cc.isGrounded)
        {
            yVelocity += Physics.gravity.y * Time.deltaTime * gravityScalar;
        }
        else
        {
            yVelocity = -1;
            if (jump)
            {
                yVelocity = jumpForce;
            }
        }
        //X-Z Plane movement
        Vector3 movement = gameObject.transform.forward * movementSpeed * Time.deltaTime * vMove + gameObject.transform.right * strafe * strafeSpeed * Time.deltaTime;
        movement.y = yVelocity*0.25f;
        cc.Move(movement);
        mainCamera.transform.position = gameObject.transform.position + gameObject.transform.forward * -3 + new Vector3(0,5,0);

        if (dead)
        {
            gameObject.transform.position = respawnPoint;
            cc.transform.position = respawnPoint;
            dead = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Checkpoint")
        {
            Debug.Log("Checkpoint!");
            respawnPoint = col.transform.position;
        }
    }

    public void kill()
    {
        Debug.Log("Died");
        dead = true;

    }
}
