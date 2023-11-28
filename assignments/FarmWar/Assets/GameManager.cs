using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    //Public Variables
    public GameObject baseTile;
    public GameObject penTile;
    public GameObject farmhouseTile;
    public GameObject storageTile;
    public GameObject sheep1PenTile;
    public GameObject sheep2PenTile;
    public GameObject sheep3PenTile;
    public GameObject chicken1PenTile;
    public GameObject chicken2PenTile;
    public GameObject chicken3PenTile;
    public GameObject chicken4PenTile;
    public GameObject cow1PenTile;
    public GameObject cow2PenTile;
    public GameObject cow3PenTile;
    public GameObject forestTile;
    public Material upgrade1;
    public Material upgrade1over;
    public Material upgrade2;
    public Material upgrade2over;
    public GameObject woodsman;
    public static event Action<float> gameWon;

    public static int[] origin = { -20, -20 };
    float startTime;

    //Tile Map Variables
    GameObject[,] tiles;
    GameObject selectedTile;
    GameObject selectedWorker;

    //Game Elements
    int gold = 100;
    int wood = 100;
    int animalSupply = 0;
    int animalSupplyCap = 0;
    int farmhouseCount = 1;
    int cowSupplyIncrease = 2;
    int cowCount;
    int chickenSupplyIncrease = 1;
    int chickenCount;
    int sheepSupplyIncrease = 1;
    int sheepCount;
    int farmhouseCapIncrease = 4;
    int[] storageLocation = { 2, 2 };
    public float timeSinceIncome;

    //Object Pooling
    GameObject[] employeePool;
    int currentNum;
    private void Awake()
    {
        if (sharedInstance != null) Debug.Log("THERE'S ALREADY A GAMEMANAGER!!!");
        sharedInstance = this;
        tiles = new GameObject[5, 5];
        //Create Array of Plots
        //Make Center One a farmhouse
        int tileAmount = 5;
        for(int i = 0; i < tileAmount; i++)
        {
            for (int j = 0; j < tileAmount; j++)
            {
                GameObject tile;
                if (i == 2 && j == 2)
                {
                    tile = Instantiate(storageTile, new Vector3(i * 10 + origin[0], 0, j * 10 + origin[1]), Quaternion.identity);
                    tile.GetComponent<TileScript>().setCoords(i, j);
                }
                if (i == 0 || i == 4 || j==0 || j==4)
                {
                    tile = Instantiate(forestTile, new Vector3(i * 10 + origin[0], 0, j * 10 + origin[1]), Quaternion.identity);
                    tile.GetComponent<TileScript>().setCoords(i, j);
                }
                else
                {
                    tile = Instantiate(baseTile, new Vector3(i * 10 + origin[0], 0, j * 10 + origin[1]), Quaternion.identity);
                    tile.GetComponent<TileScript>().setCoords(i, j);
                }
                tiles[i, j] = tile;
                
            }
        }
        //Creating the Lumberjacks
        employeePool = new GameObject[10];
        for(int i = 0; i < 10; i++)
        {
            GameObject woods = Instantiate(woodsman, new Vector3(0,0,5), Quaternion.identity);
            employeePool[i] = woods;
            woods.SetActive(false);
        }
        employeePool[0].SetActive(true);
        currentNum = 0;
    }

    private void Start()
    {
        timeSinceIncome = Time.time;
        startTime = Time.time;
    }
    private void Update()
    {
        if(Time.time - timeSinceIncome >= 30)
        {
            int income = calculateIncome();
            gold += income;
            timeSinceIncome = Time.time;
        }

        if(gold >= 10000)
        {
            gameWon?.Invoke(startTime);
        }
    }

    public void createTile(string tileType)
    {
        int supplyDifference = animalSupplyCap - animalSupply;
        bool approval = true;
        //CheckIfWeCanMoveForward
        {
        if (tileType == "sheep1Pen" || tileType == "chicken1Pen" || tileType == "addSheep" || tileType == "addChicken")
        {
            if (supplyDifference <= 0)
            {
                approval = false;
            }
        }
        else if (tileType == "cow1Pen" || tileType == "addCow")
        {
            if (supplyDifference <= 1)
            {
                approval = false;
            }
        }
        }
        //CheckIfWeHaveTheResources
        {
            if(tileType == "sheep1Pen" || tileType == "addSheep")
            {
                if (wood < 200)
                {
                    approval = false;
                }
            }
            if (tileType == "chicken1Pen" || tileType == "addChicken")
            {
                if (wood < 75)
                {
                    approval = false;
                }
            }
            if (tileType == "cow1Pen" || tileType == "addCow")
            {
                if (wood < 350)
                {
                    approval = false;
                }
            }
            if (tileType == "pen")
            {
                if (wood < 75)
                {
                    approval = false;
                }
            }
            if (tileType == "farmhouse")
            {
                if (wood < 50)
                {
                    approval = false;
                }
            }
            if (tileType == "storage")
            {
                if (wood < 200)
                {
                    approval = false;
                }
            }
        }
        //If We can't? Ignore EVERYTHING below
        int upgradeCount = selectedTile.GetComponent<TileScript>().upgradeNumber();
        GameObject tile = null;
        int tileX = selectedTile.GetComponent<TileScript>().getX();
        int tileY = selectedTile.GetComponent<TileScript>().getY();

        if (approval)
        {
            UnityEngine.Object.Destroy(selectedTile);
            {
                if (tileType == "basic")
                {
                    tile = Instantiate(baseTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                    upgradeCount = 0;
                }
                if (tileType == "farmhouse")
                {
                    tile = Instantiate(farmhouseTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                    animalSupplyCap += farmhouseCapIncrease;
                    farmhouseCount++;
                    wood -= 50;
                }
                if (tileType == "pen")
                {
                    tile = Instantiate(penTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                    wood -= 75;
                }
                if (tileType == "storage")
                {
                    tile = Instantiate(storageTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                    wood -= 200;
                }
                if (tileType == "sheep1Pen")
                {
                    tile = Instantiate(sheep1PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                    tile.GetComponent<TileScript>().add(1);
                    animalSupply += sheepSupplyIncrease;
                    sheepCount++;
                    wood -= 200;
                }
                if (tileType == "chicken1Pen")
                {
                    tile = Instantiate(chicken1PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                    tile.GetComponent<TileScript>().add(1);
                    animalSupply += chickenSupplyIncrease;
                    chickenCount++;
                    wood -= 75;
                }
                if (tileType == "cow1Pen")
                {
                    tile = Instantiate(cow1PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                    tile.GetComponent<TileScript>().add(1);
                    animalSupply += cowSupplyIncrease;
                    cowCount++;
                    wood -= 350;
                }
                if (tileType == "addSheep")
                {
                    Debug.Log(selectedTile.GetComponent<TileScript>().getCount());
                    if (selectedTile.GetComponent<TileScript>().getCount() == 1)
                    {
                        tile = Instantiate(sheep2PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(2);
                        tileType = "sheep2Pen";
                        animalSupply += sheepSupplyIncrease;
                        sheepCount++;
                        wood -= 200;
                    }
                    else if (selectedTile.GetComponent<TileScript>().getCount() == 2)
                    {
                        tile = Instantiate(sheep3PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(3);
                        tileType = "sheep3Pen";
                        animalSupply += sheepSupplyIncrease;
                        sheepCount++;
                        wood -= 200;
                    }
                    else
                    {
                        tile = Instantiate(sheep3PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(3);
                        tileType = "sheep3Pen";
                    }
                }
                if (tileType == "addChicken")
                {
                    if (selectedTile.GetComponent<TileScript>().getCount() == 1)
                    {
                        tile = Instantiate(chicken2PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(2);
                        tileType = "chicken2Pen";
                        animalSupply += chickenSupplyIncrease;
                        chickenCount++;
                        wood -= 75;
                    }
                    else if (selectedTile.GetComponent<TileScript>().getCount() == 2)
                    {
                        tile = Instantiate(chicken3PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(3);
                        tileType = "chicken3Pen";
                        animalSupply += chickenSupplyIncrease;
                        chickenCount++;
                        wood -= 75;
                    }
                    else if (selectedTile.GetComponent<TileScript>().getCount() == 3)
                    {
                        tile = Instantiate(chicken4PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(4);
                        tileType = "chicken4Pen";
                        animalSupply += chickenSupplyIncrease;
                        chickenCount++;
                        wood -= 75;
                    }
                    else
                    {
                        tile = Instantiate(chicken4PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(4);
                        tileType = "chicken4Pen";
                    }

                }
                if (tileType == "addCow")
                {

                    if (selectedTile.GetComponent<TileScript>().getCount() == 1)
                    {
                        tile = Instantiate(cow2PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(2);
                        tileType = "cow2Pen";
                        animalSupply += cowSupplyIncrease;
                        cowCount++;
                        wood -= 350;
                    }
                    else if (selectedTile.GetComponent<TileScript>().getCount() == 2)
                    {
                        tile = Instantiate(cow3PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(3);
                        tileType = "cow3Pen";
                        animalSupply += cowSupplyIncrease;
                        cowCount++;
                        wood -= 350;
                    }
                    else
                    {
                        tile = Instantiate(cow3PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(3);
                        tileType = "cow3Pen";
                    }

                }
                if (tileType == "sellSheep")
                {
                    int count = selectedTile.GetComponent<TileScript>().getCount();

                    if (count == 1)
                    {
                        tile = Instantiate(penTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tileType = "pen";
                        animalSupply -= sheepSupplyIncrease;
                        sheepCount--;
                        wood += 200;
                    }
                    if (count == 2)
                    {
                        tile = Instantiate(sheep1PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(1);
                        tileType = "sheep1Pen";
                        animalSupply -= sheepSupplyIncrease;
                        sheepCount--;
                        wood += 200;
                    }
                    if (count == 3)
                    {
                        tile = Instantiate(sheep2PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(2);
                        tileType = "sheep2Pen";
                        animalSupply -= sheepSupplyIncrease;
                        sheepCount--;
                        wood += 200;
                    }
                }
                if (tileType == "sellCow")
                {
                    int count = selectedTile.GetComponent<TileScript>().getCount();

                    if (count == 1)
                    {
                        tile = Instantiate(penTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tileType = "pen";
                        animalSupply -= 2;
                        cowCount--;
                        wood += 350;
                    }
                    if (count == 2)
                    {
                        tile = Instantiate(cow1PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(1);
                        tileType = "cow1Pen";
                        animalSupply -= 2;
                        cowCount--;
                        wood += 350;
                    }
                    if (count == 3)
                    {
                        tile = Instantiate(cow2PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(2);
                        tileType = "cow2Pen";
                        animalSupply -= 2;
                        cowCount--;
                        wood += 350;
                    }
                }
                if (tileType == "sellChicken")
                {
                    int count = selectedTile.GetComponent<TileScript>().getCount();

                    if (count == 1)
                    {
                        tile = Instantiate(penTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tileType = "pen";
                        animalSupply -= chickenSupplyIncrease;
                        chickenCount--;
                        wood += 75;
                    }
                    if (count == 2)
                    {
                        tile = Instantiate(chicken1PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(1);
                        tileType = "chicken1Pen";
                        animalSupply -= chickenSupplyIncrease;
                        chickenCount--;
                        wood += 75;
                    }
                    if (count == 3)
                    {
                        tile = Instantiate(chicken2PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(2);
                        tileType = "chicken2Pen";
                        animalSupply -= chickenSupplyIncrease;
                        chickenCount--;
                        wood += 75;
                    }
                    if (count == 4)
                    {
                        tile = Instantiate(chicken3PenTile, new Vector3(tileX * 10 + origin[0], 0, tileY * 10 + origin[1]), Quaternion.identity);
                        tile.GetComponent<TileScript>().add(3);
                        tileType = "chicken3Pen";
                        animalSupply -= chickenSupplyIncrease;
                        chickenCount--;
                        wood += 75;
                    }
                }
            }

            tile.GetComponent<TileScript>().setCoords(tileX, tileY);
            selectedTile = tile;
            if (upgradeCount > 0)
            {
                for (int i = 0; i < upgradeCount; i++)
                {
                    upgradeTileNoResource();
                }
            }
            tiles[tileX, tileY] = tile;
            UIManager.sharedInstance.activateTile(tileType);
            selectedTile = tile;
        }
    }
    public void setSelectedTile(GameObject tile)
    {
        selectedTile = tile;

        if(tile == null)
        {
            UIManager.sharedInstance.deactivateAll();
        }
    }
    public void setSelectedWorker(GameObject worker)
    {
        selectedWorker = worker;
    }
    public void upgradeTile()
    {
        bool approval = true;

        if(wood < 400)
        {
            approval = false;
        }
        if (approval)
        {
            int upgradeNum = selectedTile.GetComponent<TileScript>().upgradeNumber();
            if (upgradeNum == 0)
            {
                selectedTile.GetComponent<TileScript>().notOver = upgrade1;
                selectedTile.GetComponent<TileScript>().mousedover = upgrade1over;
                selectedTile.GetComponent<Renderer>().material = upgrade1;
                selectedTile.GetComponent<TileScript>().upgrade(1);
                wood -= 400;
            }
            if (upgradeNum == 1)
            {
                selectedTile.GetComponent<TileScript>().notOver = upgrade2;
                selectedTile.GetComponent<TileScript>().mousedover = upgrade2over;
                selectedTile.GetComponent<Renderer>().material = upgrade2;
                selectedTile.GetComponent<TileScript>().upgrade(2);
                wood -= 400;
            }
        }
    }
    public void upgradeTileNoResource()
    {

            int upgradeNum = selectedTile.GetComponent<TileScript>().upgradeNumber();
            if (upgradeNum == 0)
            {
                selectedTile.GetComponent<TileScript>().notOver = upgrade1;
                selectedTile.GetComponent<TileScript>().mousedover = upgrade1over;
                selectedTile.GetComponent<Renderer>().material = upgrade1;
                selectedTile.GetComponent<TileScript>().upgrade(1);
            }
            if (upgradeNum == 1)
            {
                selectedTile.GetComponent<TileScript>().notOver = upgrade2;
                selectedTile.GetComponent<TileScript>().mousedover = upgrade2over;
                selectedTile.GetComponent<Renderer>().material = upgrade2;
                selectedTile.GetComponent<TileScript>().upgrade(2);
                wood -= 400;
            }
    }

    public GameObject getSelectedWorker()
    {
        return selectedWorker;
    }

    public void addWood(int amount)
    {
        wood += amount;
    }
    public void addGold(int amount)
    {
        gold += amount;
    }

    public int getWood() { return wood; }
    public int getGold() { return gold; }
    public int getAnimal() { return animalSupply; }
    public int getAnimalLimit() { return animalSupplyCap; }
    public GameObject getStorage()
    {
        return tiles[2, 2];
    }

    public void summonWoodsman()
    {
        if (currentNum < 9 && wood >=100)
        {
            currentNum += 1;
            employeePool[currentNum].SetActive(true);
            wood -= 100;
        }
    }

    int calculateIncome()
    {
        int income = 0;
        for(int i = 0; i < tiles.GetLength(0); i++)
        {
            for(int j = 0; j < tiles.GetLength(1);j++)
            {
                income += tiles[i, j].GetComponent<TileScript>().calculateIncome();
            }
        }
        return income;
    }
}
