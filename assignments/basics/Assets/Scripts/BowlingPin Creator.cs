using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPinCreator : MonoBehaviour
{
    public GameObject bowlingPin;
    // Start is called before the first frame update
    void Start()
    {
        
        //Version 2
        ///for (int i = 0; i < 4; i++)
        //{
        //    Instantiate(bowlingPin, new Vector3(baseX - i * 8, 0.25f, baseZ), Quaternion.identity);
        //}
        //for (double i = 0.5; i < 3.5; i++)
        //{
        //    Instantiate(bowlingPin, new Vector3(baseX - i * 8, 0.25f, baseZ - 5), Quaternion.identity);
        //}
        //for (double i = 1; i < 3; i++)
        //{
        //   Instantiate(bowlingPin, new Vector3(baseX - i * 8, 0.25f, baseZ + 10), Quaternion.identity);
        //}
        //for (double i = 1.5; i < 2.5; i++)
        //{
        //    Instantiate(bowlingPin, new Vector3(baseX - i * 8, 0.25f, baseZ + 15), Quaternion.identity);
        //}
        ///
        int pinRowNumber = 4;
        int distanceBetweenX = 8;
        int distanceBetweenZ = 5;
        float startingX = 12f;
        int startingZ = -88;
        float startingY = 5.25f;

        // Row, Column, Starting offset
        // 4, 4, 0
        // 3, 3, .5
        // 2, 2, 1
        // 1, 1, 1.5


    for(int i = 0; i < pinRowNumber; i++)
        {
            for(float j = i*0.5f; j<(pinRowNumber-i*0.5f); j++)
            {
                Instantiate(bowlingPin, new Vector3(startingX - j * distanceBetweenX, startingY, startingZ + i * distanceBetweenZ), Quaternion.identity);
            }
        }

    }

}
