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
    }

}
