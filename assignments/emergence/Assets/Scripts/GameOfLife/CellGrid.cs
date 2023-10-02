using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    // Start is called before the first frame update
    public int xLength;
    public int zLength;
    GameObject[,] cellArray;
    public GameObject cellPrefab;
    int timeCounter = 1;
    float timeAccum = 0;
    int speed = 10;
    public int initialX;
    public int initialZ;

    void Start()
    {
        this.cellArray = createGrid(xLength, zLength);
    }
    void Update()
    {
        timeAccum += Time.deltaTime;
        if (timeAccum > timeCounter*speed)
        {
            for (int x = 0; x < xLength; x++)
            {
                for (int z = 0; z < zLength; z++)
                {
                    cellArray[x, z].GetComponentInChildren<Cell>().simulateLife(cellArray);
                }
            }

            for (int x = 0; x < xLength; x++)
            {
                for (int z = 0; z < zLength; z++)
                {
                    cellArray[x, z].GetComponentInChildren<Cell>().updateCurrentState();
                }
            }
            timeCounter++;
        }
    }

    GameObject[,] createGrid(int xLength, int zLength)
    {
        GameObject[,] gridOfCells = new GameObject[xLength, zLength];
        for (int x = 0; x < xLength; x++)
        {
            for (int z = 0; z < zLength; z++)
            {
                gridOfCells[x,z] = createCell(x, z);
            }
        }
        return gridOfCells;
    }
    GameObject createCell(int x, int z)
    {
        GameObject cell = Instantiate(cellPrefab, new Vector3(x * 8 + 1f+initialX, 1, z * 8 + 1f+initialZ), Quaternion.identity);
        int[] pos = new int[2];
        pos[0] = x;
        pos[1] = z;
        cell.GetComponentInChildren<Cell>().cellPos = pos;
        int num = Random.Range(0, 2);
        if (num == 0) {
            cell.GetComponentInChildren<Cell>().currentState = "On";
        }
        else
        {
            cell.GetComponentInChildren<Cell>().currentState = "Off";
        }

        return cell;
    }
}
    
    

