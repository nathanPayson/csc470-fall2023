using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using JetBrains.Annotations;

public class MovingPlatform : MonoBehaviour
{
    public int movementSpeed = 10;
    public Vector3[] locations;
    public int currentTarget = -1;
    int targetCount;
    public int transitionPeriod = 3;
    bool transitioning = false;
    float timeSinceTransitionBegan;
    Vector3 movement = Vector3.zero;
    GameObject connected = null;

    // Start is called before the first frame update
    void Start()
    {
        targetCount = locations.Length-1;
        if (currentTarget == -1) currentTarget = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transitioning)
        {
            movement = Vector3.zero;
           if(Time.time -  timeSinceTransitionBegan > transitionPeriod)
            {
                transitioning = false;
                currentTarget += 1;
                if(currentTarget > targetCount)
                {
                    currentTarget = 0;
                }
            }
        }
        else
        {
            Vector3 unitVector = calculateUnitVector(locations[currentTarget], gameObject.transform.position);
            float distance = calculateDistance(locations[currentTarget], gameObject.transform.position);
            if(movementSpeed * Time.deltaTime > distance)
            {
                gameObject.transform.position = locations[currentTarget];
                transitioning = true;
                timeSinceTransitionBegan = Time.time;
            }
            else
            {
                movement = unitVector * movementSpeed * Time.deltaTime;
                gameObject.transform.position += movement;
                if(connected != null)
                {
                    connected.GetComponent<CharacterController>().Move(movement);
                }
            }
        }
        //If In Transition Wait
        //If We are Done - Change Target
        //Else Keep Transitioning
        //Else Move towards Target{
        //CalculateUUnitVector()
        //If unit Vector * movement speed *time.deltaTime> distance
           //gameObject.position = target positon
        //Else gameObject.position += unitVector * movementSpeed*time.deltaTime
        //Vector3 targetLocation = locations[currentTarget];
    }
    Vector3 calculateUnitVector(Vector3 destination, Vector3 location)
    {
        float xDist = destination.x - location.x;
        float yDist = destination.y - location.y;
        float zDist = destination.z - location.z;
        float distance = calculateDistance(destination, location);
        return new Vector3(xDist / distance, yDist / distance, zDist / distance);
    }

    float calculateDistance(Vector3 destination, Vector3 location)
    {
        float xDist = location.x - destination.x;
        float yDist = location.y - destination.y;
        float zDist = location.z - destination.z;
        float sum = xDist * xDist + yDist * yDist + zDist * zDist;
        return Mathf.Sqrt(sum);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerPlat")
        {
            connected = other.gameObject;
        }
    }
/*    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerPlat")
        {
            Debug.Log("Ping Staying");
            other.transform.position += movement;
            connected = other.gameObject;
        }
    }*/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerPlat")
        {
            connected = null;
        }
    }
}
