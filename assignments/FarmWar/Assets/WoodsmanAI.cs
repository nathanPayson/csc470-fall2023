using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class WoodsmanAI : MonoBehaviour
{
    GameObject forest;
    int movementSpeed = 4;
    public Animator animator;
    bool atFarm = false;
    bool atTree = false;
    int currentDest = 0; //0 = Moving/idle, 1 = Forest, 2 = Shed Move, 3 = Shed
    GameObject storage;
    float timeSinceWood;
    float woodAmount = 0;
    void Start()
    {
        storage = GameManager.sharedInstance.getStorage(); 
    }

    void Update()
    {
        if (currentDest == 0)
        {
            move0();
        }
        else if (currentDest == 1)
        {
            move1();
            obtainWood();
        }
        else if (currentDest == 2)
        {
            move2();
        }
        else if (currentDest == 3)
        {
            move3();
            storeWood();
        }
    }

    public void setForest(GameObject tile)
    {
        forest = tile;
    }
    void move0()
    {
        float distance = 0;
        if(forest != null)
        {
            Vector3 unitVector = calculateUnitVector(forest.transform.position, transform.position);
            distance = calculateDistance(forest.transform.position, transform.position);
            if(movementSpeed * Time.deltaTime > distance || distance < 0.05)
            {
                gameObject.transform.position = forest.transform.position;
                atTree = true;
                distance = 0;
                currentDest = 1;
                timeSinceWood = Time.time;
            }
            else
            {
                Vector3 movement = unitVector * movementSpeed * Time.deltaTime;
                gameObject.transform.position += movement;
                gameObject.transform.forward = unitVector;
                atTree = false;
            }
        }
        animator.SetFloat("Speed", distance);
        animator.SetInteger("State", currentDest);

        //Move up to 5 distance in the direction of Forest
        //If already at forest and tree has wood left to chop, instead chop wood
    }
    void move1()
    {
        float distance = 0;
        if (forest != null)
        {
            Vector3 unitVector = calculateUnitVector(forest.transform.position, transform.position);
            distance = calculateDistance(forest.transform.position, transform.position);
            if (transform.position == forest.transform.position)
            {
                //Check if forest has wood left
                //Chop Wood
                atTree = true;
            }
            else if (movementSpeed * Time.deltaTime > distance || distance < 0.05)
            {
                gameObject.transform.position = forest.transform.position;
                atTree = true;
                distance = 0;
            }
            else
            {
                Vector3 movement = unitVector * movementSpeed * Time.deltaTime;
                gameObject.transform.position += movement;
                gameObject.transform.forward = unitVector;
                atTree = false;
            }
        }
        animator.SetFloat("Speed", distance);
        animator.SetInteger("State", currentDest);

        //Move up to 5 distance in the direction of Forest
        //If already at forest and tree has wood left to chop, instead chop wood
    }
    void move2()
    {
        float distance = 0;
        if (storage != null)
        {

            Vector3 unitVector = calculateUnitVector(storage.transform.position, transform.position);
            distance = calculateDistance(storage.transform.position, transform.position);
            if (movementSpeed * Time.deltaTime > distance || distance < 0.05)
            {
                gameObject.transform.position = storage.transform.position;
                atFarm = true;
                distance = 0;
                currentDest = 3;
                timeSinceWood = Time.time;
            }
            else
            {
                Vector3 movement = unitVector * movementSpeed * Time.deltaTime;
                gameObject.transform.position += movement;
                gameObject.transform.forward = unitVector;
                atFarm = false;
            }
        }
        animator.SetFloat("Speed", distance);
        animator.SetInteger("State", currentDest);

        //Move up to 5 distance in the direction of Forest
        //If already at forest and tree has wood left to chop, instead chop wood
    }
    void move3()
    {
        float distance = 0;
        if (forest != null)
        {
            Vector3 unitVector = calculateUnitVector(storage.transform.position, transform.position);
            distance = calculateDistance(storage.transform.position, transform.position);
            if (transform.position == storage.transform.position)
            {
                //Check if forest has wood left
                //Chop Wood
                atFarm = true;
            }
            else if (movementSpeed * Time.deltaTime > distance || distance < 0.05)
            {
                gameObject.transform.position = storage.transform.position;
                atFarm = true;
                distance = 0;
            }
            else
            {
                Vector3 movement = unitVector * movementSpeed * Time.deltaTime;
                gameObject.transform.position += movement;
                gameObject.transform.forward = unitVector;
                atFarm = false;
            }
        }
        animator.SetFloat("Speed", distance);
        animator.SetInteger("State", currentDest);

        //Move up to 5 distance in the direction of Forest
        //If already at forest and tree has wood left to chop, instead chop wood
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on Woodsman");
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameManager.sharedInstance.GetComponent<GameManager>().setSelectedTile(null);
            GameManager.sharedInstance.GetComponent<GameManager>().setSelectedWorker(gameObject);
        }
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

    void obtainWood()
    {
        //Collect Wood every second
        if(Time.time - timeSinceWood > 1)
        {
            woodAmount += 10;
            timeSinceWood = Time.time;
        }
        //if wood amount = 50
        if (woodAmount >= 50){
            currentDest = 2;
        }
    }

    void storeWood()
    {
        //Collect Wood every second
        if (Time.time - timeSinceWood > 1)
        {
            woodAmount -=10;
            timeSinceWood = Time.time;
            GameManager.sharedInstance.addWood(10);
        }
        //if wood amount = 50
        if (woodAmount <= 0)
        {
            currentDest = 0;
        }
    }
}
