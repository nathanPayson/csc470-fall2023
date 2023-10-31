using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : MonoBehaviour
{
    GameObject bigBoss;
    public GameObject boss;
    public GameObject flier;
    // Start is called before the first frame update
    void Start()
    {
        bigBoss = Instantiate(boss, new Vector3(), Quaternion.identity);
        bigBoss.GetComponent<BossAI>().player = flier;
        bigBoss.GetComponent<EnemyAI>().setHealth(50);
        bigBoss.transform.position = new Vector3(300, 0, 300);
        GameManager.SharedInstance.activateBoss();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
