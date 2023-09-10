using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceGeneratorScript : MonoBehaviour
{
    public GameObject man;
    public GameObject woman;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 40; i++)
        {
            generateMan(-31, -60,67,-72,90);
            generateWoman(-31, -60, 67, -72,90);
            generateMan(31, 60, 67, -72,270);
            generateWoman(31, 60, 67, -72,270);
        }
    }

    void generateMan(int xMin, int xMax, int zMin, int zMax, int yRot)
    {
        GameObject manClone = Instantiate(man, new Vector3(Random.RandomRange(xMin, xMax), 1, Random.RandomRange(zMin, zMax)), Quaternion.identity);
        manClone.transform.Rotate(0, yRot, 0);
    }

    void generateWoman(int xMin, int xMax, int zMin, int zMax, int yRot)
    { 
        GameObject womanClone = Instantiate(woman, new Vector3(Random.RandomRange(xMin, xMax), 1, Random.RandomRange(zMin, zMax)), Quaternion.identity);
        womanClone.transform.Rotate(0, yRot, 0);
    }
}
