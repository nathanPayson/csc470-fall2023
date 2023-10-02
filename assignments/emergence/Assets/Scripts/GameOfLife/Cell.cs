using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int[] cellPos;
    public string currentState = "On";
    public string futureState = "Off";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState.Equals("On"))
        {
            //Change Color and Tag to on
            Renderer rend = gameObject.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.white);
            gameObject.tag = "On Cell";

        }
        if (currentState.Equals("Off"))
        {
            Renderer rend = gameObject.GetComponent<Renderer>();
            rend.material.SetColor("_Color", Color.red);
            gameObject.tag = "Off Cell";
        }
    }
    public void simulateLife(GameObject[,] cellArray)
    {
        int neighborCount = findNeighborCount(cellArray);
        if (neighborCount > 3 && currentState.Equals("On"))
        {
            futureState = "Off";
        }
        else if (neighborCount < 2 && currentState.Equals("On"))
        {
            futureState = "Off";
        }
        else if (neighborCount == 3)
        {
            futureState = "On";
        }
        else if (neighborCount == 2 && currentState.Equals("On"))
        {
            futureState = "On";
        }
        else
        {
            futureState = "Off";
        }
 
    }
    public void updateCurrentState()
    {
        this.currentState = this.futureState;
        this.futureState = "Off";
    }
    public void setCurrentState(string state)
    {
        currentState = state;
    }
    public string getCurrentState() {
        return currentState;
    }

    int findNeighborCount(GameObject[,] cellArray)
    {
        int neighborCount = 0;
        //CheckIfTopLeftCornerIsOn
        if (cellPos[0] > 0 && cellPos[1] > 0)
        {
            if (cellArray[cellPos[0] - 1, cellPos[1] - 1].GetComponentInChildren<Cell>().getCurrentState().Equals("On")) {
                neighborCount += 1;
            }
        }
        //CheckIfTopCenterIsOn
        if (cellPos[0] > 0)
        {
            if (cellArray[cellPos[0] - 1, cellPos[1]].GetComponentInChildren<Cell>().getCurrentState().Equals("On"))
            {
                neighborCount += 1;
            }
        }
        //CheckIfTopLeftIsON
        if (cellPos[0] > 0 && (cellPos[1] < cellArray.GetLength(1)-1))
        {
            if (cellArray[cellPos[0] - 1, cellPos[1] + 1].GetComponentInChildren<Cell>().getCurrentState().Equals("On"))
            {
                neighborCount += 1;
            }
        }
        //CheckIfLeftCenterIsON
        if (cellPos[1] > 0)
        {
            if (cellArray[cellPos[0], cellPos[1] - 1].GetComponentInChildren<Cell>().getCurrentState().Equals("On"))
            {
                neighborCount += 1;
            }
        }
        //CheckIfRightCenterIsOn
        if (cellPos[1] < cellArray.GetLength(1)-1)
        {
            if (cellArray[cellPos[0], cellPos[1] + 1].GetComponentInChildren<Cell>().getCurrentState().Equals("On"))
            {
                neighborCount += 1;
            }
        }
        //CheckIfBottomLeftIsOn
        if (cellPos[0] < cellArray.GetLength(0)-1 && cellPos[1]>0)
        {
            if (cellArray[cellPos[0]+1, cellPos[1] - 1].GetComponentInChildren<Cell>().getCurrentState().Equals("On"))
            {
                neighborCount += 1;
            }
        }
        //CheckIfBottomCenterIsOn
        if (cellPos[0] < cellArray.GetLength(0)-1)
        {
            if (cellArray[cellPos[0] + 1, cellPos[1]].GetComponentInChildren<Cell>().getCurrentState().Equals("On"))
            {
                neighborCount += 1;
            }
        }
        //CheckIfBottomRightIsOn
        if (cellPos[0] < cellArray.GetLength(0)-1 && cellPos[1] < cellArray.GetLength(1)-1)
        {
            if (cellArray[cellPos[0] + 1, cellPos[1]+1].GetComponentInChildren<Cell>().getCurrentState().Equals("On"))
            {
                neighborCount += 1;
            }
        }
        return neighborCount;
    }

    public void setPos(int[] pos)
    {
        this.cellPos = pos;
    }
}
