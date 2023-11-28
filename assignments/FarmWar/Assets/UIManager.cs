using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class UIManager : MonoBehaviour
{
    public GameObject farmhousePanel;
    public GameObject penPanel;
    public GameObject storagePanel;
    public GameObject tilePanel;
    public GameObject cowPanel;
    public GameObject chickenPanel;
    public GameObject sheepPanel;
    public TMP_Text goldText;
    public TMP_Text woodText;
    public TMP_Text animalSupplyText;
    public TMP_Text timerText;
    public TMP_Text winText;
    public GameObject winTextObject;
    bool notWon = false;

    public static UIManager sharedInstance;


    private void Awake()
    {
        if (sharedInstance != null) Debug.Log("UI MANAGER ALREADY EXISTS");
        sharedInstance = this;
    }

    private void OnEnable()
    {
        GameManager.gameWon += winCondition;
    }
    private void OnDisable()
    {
        GameManager.gameWon -= winCondition;
    }
    public void deactivateAll()
    {
        tilePanel.SetActive(false);
        farmhousePanel.SetActive(false);
        storagePanel.SetActive(false);
        penPanel.SetActive(false);
        cowPanel.SetActive(false);
        chickenPanel.SetActive(false);
        sheepPanel.SetActive(false);

    }

    public void activateTile(string tileType)
    {
        deactivateAll();
        if(tileType == "basic")
        {
            tilePanel.SetActive(true);
        }
        if(tileType == "farmhouse")
        {
            farmhousePanel.SetActive(true);
        }
        if(tileType == "pen")
        {
            penPanel.SetActive(true);
        }
        if(tileType == "storage")
        {
            storagePanel.SetActive(true);
        }
        if (tileType == "cow1Pen" || tileType == "cow2Pen" || tileType == "cow3Pen")
        {
            cowPanel.SetActive(true);
        }
        if (tileType == "chicken1Pen" || tileType == "chicken2Pen" || tileType == "chicken3Pen"|| tileType == "chicken4Pen")
        {
            chickenPanel.SetActive(true);
        }
        if (tileType == "sheep1Pen" || tileType == "sheep2Pen" || tileType == "sheep3Pen")
        {
            sheepPanel.SetActive(true);
        }
    }

    private void Update()
    {
        setWoodText();
        setGoldText();
        setAnimalText();
        setTimerText();
    }
    void setWoodText()
    {
        woodText.SetText("Wood: " + GameManager.sharedInstance.getWood());
    }
    void setGoldText()
    {
        goldText.SetText("Gold: " + GameManager.sharedInstance.getGold());
    }
    void setAnimalText()
    {
        animalSupplyText.SetText("Animals: " + GameManager.sharedInstance.getAnimal() + "/" + GameManager.sharedInstance.getAnimalLimit());
    }
    void setTimerText()
    {
        timerText.SetText("Time till income: " + (int)(30 - (Time.time - GameManager.sharedInstance.timeSinceIncome)));
    }

    void winCondition(float time)
    {
        if (notWon)
        {
            winTextObject.SetActive(true);
            int timeToComplete = (int)(Time.time - time);
            winText.SetText("You won in " + timeToComplete + "seconds");
        }
    }
}

