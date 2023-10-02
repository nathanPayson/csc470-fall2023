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
        Instantiate(bowlingPin,new Vector3(12, 0.25f,-88),Quaternion.identity);
        Instantiate(bowlingPin, new Vector3(4, 0.25f, -88), Quaternion.identity);
        Instantiate(bowlingPin, new Vector3(-4, 0.25f, -88), Quaternion.identity);
        Instantiate(bowlingPin, new Vector3(-12, 0.25f, -88), Quaternion.identity);
        Instantiate(bowlingPin, new Vector3(8, 0.25f, -83), Quaternion.identity);
        Instantiate(bowlingPin, new Vector3(0, 0.25f, -83), Quaternion.identity);
        Instantiate(bowlingPin, new Vector3(-8, 0.25f, -83), Quaternion.identity);
        Instantiate(bowlingPin, new Vector3(4, 0.25f, -78), Quaternion.identity);
        Instantiate(bowlingPin, new Vector3(-4, 0.25f, -78), Quaternion.identity);
        Instantiate(bowlingPin, new Vector3(0, 0.25f, -73), Quaternion.identity);

        //Version 2
        ///for (int i = 0; i < 4; i++)
        //{
        //    Instantiate(bowlingPin, new Vector3(baseX - i * 8, 0.25f, baseZ), Quaternion.identity);
        //}
        //for (double i = 0.5; i < 3; i++)
        //{
        //    Instantiate(bowlingPin, new Vector3(baseX - i * 8, 0.25f, baseZ + 5), Quaternion.identity);
        //}
        //for (double i = 1; i < 3; i++)
        //{
        //   Instantiate(bowlingPin, new Vector3(baseX - i * 8, 0.25f, baseZ + 10), Quaternion.identity);
        //}
        //for (double i = 1.5; i < 2; i++)
        //{
        //    Instantiate(bowlingPin, new Vector3(baseX - i * 8, 0.25f, baseZ + 15), Quaternion.identity);
        //}
        ///
    }

}
