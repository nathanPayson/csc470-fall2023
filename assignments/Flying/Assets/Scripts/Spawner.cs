using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    float timer;
    public GameObject basicEnemy;
    public GameObject boss;
    public GameObject flier;

    GameObject[] basicEnemies;
    GameObject bigBoss;
    bool[] reached;
    private void Start()
    {
        basicEnemies = new GameObject[6];
        for(int i = 0; i < basicEnemies.Length; i++)
        {
            basicEnemies[i] = Instantiate(basicEnemy, new Vector3(), Quaternion.identity);
            basicEnemies[i].SetActive(false);
            basicEnemies[i].GetComponent<BasicGhostAI>().player = flier;
        }
        bigBoss = Instantiate(boss, new Vector3(), Quaternion.identity);
        bigBoss.GetComponent<BossAI>().player = flier;
        bigBoss.SetActive(false);
        bigBoss.GetComponent<EnemyAI>().setHealth(50);
        timer = Time.time;

        reached = new bool[6];
        for (int i = 0;i < reached.Length;i++)
        {
            reached[i] = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (flier != null)
        {
            //Spawn Enemy1 at 5 seconds
            if (Time.time - timer > 5 && !reached[0])
            {
                reached[0] = true;
                basicEnemies[0].SetActive(true);
                basicEnemies[0].GetComponent<BasicGhostAI>().resetTimer();
                basicEnemies[0].transform.position += new Vector3(-420, 100, 0);
            }

            //Spawn Enemy2 at 10 seconds
            if (Time.time - timer > 10 && !reached[1])
            {
                reached[1] = true;
                basicEnemies[1].SetActive(true);
                basicEnemies[1].GetComponent<BasicGhostAI>().resetTimer();
                basicEnemies[1].transform.position = new Vector3(-450, 100, 0);
            }

            //Spaawn Enemy3 and 4 at 20 seconds
            if (Time.time - timer > 18 && !reached[2])
            {
                reached[2] = true;
                basicEnemies[2].SetActive(true);
                basicEnemies[2].transform.position = new Vector3(-570, 200, 0);
                basicEnemies[2].GetComponent<BasicGhostAI>().resetTimer();
                basicEnemies[3].SetActive(true);
                basicEnemies[3].transform.position = new Vector3(-490, 200, 0);
                basicEnemies[3].GetComponent<BasicGhostAI>().resetTimer();
            }

            //Spawn Enemy5 and 6 at 25 seconds
            if (Time.time - timer > 23 && !reached[3])
            {
                reached[3] = true;
                basicEnemies[4].SetActive(true);
                basicEnemies[4].transform.position = new Vector3(-420, 150, 0);
                basicEnemies[4].GetComponent<BasicGhostAI>().resetTimer();
                basicEnemies[5].SetActive(true);
                basicEnemies[5].transform.position = new Vector3(-540, 150, 0);
                basicEnemies[5].GetComponent<BasicGhostAI>().resetTimer();
            }

            //If Flier reaches a certain point, despawn enemies
            if (flier.transform.position.z >= 0 && !reached[4])
            {
                reached[4] = true;
                for (int i = 0; i < basicEnemies.Length; i++)
                {
                    if (basicEnemies[i] != null)
                    {
                        basicEnemies[i].SetActive(false);
                    }
                }
            }

            //When flier rounds curve spawn boss
            if (flier.transform.position.z >= 0 && flier.transform.position.x >= 0 && !reached[5])
            {
                reached[5] = true;
                bigBoss.transform.position = new Vector3(1500, 150, 1500);
                bigBoss.SetActive(true);
                GameManager.SharedInstance.activateBoss();
            }
            if (flier.transform.position.x >= 19000)
            {
                Object.Destroy(flier);
            }
        }
    }
}
