using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject rainPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            generateTree();
        }
    }

    void generateTree()
    {
        float x = Random.Range(-50,50);
        float y = 0;
        float z = Random.Range(-50,50);
        Vector3 pos = new Vector3(x,y,z);
        Instantiate(treePrefab, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Random.Range(-50, 50);
        float y = 20;
        float z = Random.Range(-50, 50);
        Vector3 pos = new Vector3(x, y, z);
        GameObject rain = Instantiate(rainPrefab, pos, Quaternion.identity);
        Renderer rainRend = rain.GetComponent<Renderer>();
        rainRend.material.color = new Color(Random.value, Random.value, Random.value);
        Destroy(rain, 2);
    }
}
