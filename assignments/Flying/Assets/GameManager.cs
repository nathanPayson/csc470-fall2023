using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public static GameManager SharedInstance;
    public TMP_Text leftText;
    int rings = 0;
    //Accidentally a bit confusing below....
    public TMP_Text rightText;
    float timerText = 80;
    public GameObject winPanel;
    public GameObject losePanel;
    int bossHealth = 50;
    int playerHealth;
    bool bossDefeated = false;
    bool bossActive = false;
    bool playerDead = false;
    bool jewelFound = false;
    int difficultySetting = -1;
    bool forcedloss = false;
    bool scene1Initiated = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (SharedInstance != null) Debug.Log("There should only be one GameManager!");
        SharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Part1")
        {
            if (scene1Initiated) {
            updateTimer();
            }
            if (timerText == 0)
            {
                if (rings >= 25)
                {
                    winPanel.SetActive(true);
                }
                if (rings < 25)
                {
                    losePanel.SetActive(true);
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Part2" || SceneManager.GetActiveScene().name == "Boss Test")
        {
            //Win/Loss Conditions Check
            if (bossHealth<=1)
            {
                winPanel.SetActive(true);
                bossDefeated = true;
            }
            else if(playerHealth <= 0 && !bossDefeated){
                losePanel.SetActive(true);
            }
            //UI Updates
            if(bossActive == true)
            {
                rightText.text = "Boss Health: " + (bossHealth-1) + "/100";
            }
            else{
                rightText.text = "";
            }
            leftText.text = "Player Health: " + playerHealth;
        }
        if (playerDead)
        {
            losePanel.SetActive(true);
        }
        if(SceneManager.GetActiveScene().name == "Part3")
        {
            
            leftText.text = "Press Esc to Leave";
            if (jewelFound)
            {
                winPanel.SetActive(true);
                rightText.text = "What Happens Next? TBD";
            }
            else if(forcedloss)
            {
                losePanel.SetActive(true);
            }

        }
        
    }

    public void updateRings()
    {
        rings++;
        leftText.SetText("Rings: " + rings + "/25");
    }

    void updateTimer()
    {
        if (timerText > 0) timerText -= Time.deltaTime;
        else timerText = 0;

        rightText.SetText(((int)(timerText * 10) / 10.0) + " Seconds");
    }

    public void loadButtonScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);


    }
    public void setBossHealth(int bossHealth)
    {
        this.bossHealth = bossHealth;
    }
    public void setPlayerHealth(int playerHealth)
    {
        this.playerHealth = playerHealth;
    }
    public void activateBoss()
    {
        bossActive = true;
    }
    public void playerDied()
    {
        playerDead = true;
    }
    public void foundJewel()
    {
        jewelFound = true;
    }
    public void setDifficult(int difficult)
    {
        difficultySetting = difficult;
    }
    public int getDifficulty()
    {
        return difficultySetting;
    }
    public void forceLoss()
    {
        forcedloss = true;
    }
    public void setTimer(int speed)
    {
        timerText = speed;
    }
    public void initiateScene1()
    {
        scene1Initiated = true;
    }
}
