using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Material notOver;
    public Material mousedover;
    public Renderer bodyRenderer;
    public string tileType;
    int xPos, yPos;
    int count = 0;
    int upgradeCount = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) == true)
        {
            UIManager.sharedInstance.deactivateAll();
            UIManager.sharedInstance.activateTile(tileType);
        }
    }
    private void OnMouseOver()
    {
       bodyRenderer.material = mousedover;
    }

    private void OnMouseExit()
    {
        bodyRenderer.material = notOver;
    }

    public void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            Debug.Log("Arrived");
            UIManager.sharedInstance.deactivateAll();
            UIManager.sharedInstance.activateTile(tileType);
            GameManager.sharedInstance.GetComponent<GameManager>().setSelectedTile(gameObject);

            //Debug.Log("X: " + xPos + " Y: " + yPos);

            if (tileType == "forest")
            {
                GameObject worker = GameManager.sharedInstance.GetComponent<GameManager>().getSelectedWorker();
                if (worker != null)
                {
                    worker.GetComponent<WoodsmanAI>().setForest(gameObject);
                }
            }
            else
            {
                GameManager.sharedInstance.GetComponent<GameManager>().setSelectedWorker(null);
            }
        }
        else
        {
            Debug.Log("Clicked UI");
            
        }
        if (Input.GetMouseButton(0))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var go in raycastResults)
                {
                    Debug.Log(go.gameObject.name, go.gameObject);
                }
            }
        }
    }

    public void setCoords(int x, int y)
    {
        this.xPos = x;
        this.yPos = y;  
    }

    public int getX()
    {
        return xPos;
    }
    public int getY()
    {
        return yPos;
    }

    public void add(int num)
    {
        count=num;
    }
    public int getCount()
    {
        return count;
    }
    public void upgrade(int num)
    {
        upgradeCount=num;
    }
    public int upgradeNumber()
    {
        return upgradeCount;
    }

    public int calculateIncome()
    {
        int income = 0;
        if (tileType.Length >= 5 && tileType.Substring(0,5) == "sheep")
        {
            income = (int) (250 * count * (1 + upgradeCount * 0.5));
        }
        else if(tileType.Length >= 3 && tileType.Substring(0,3) == "cow")
        {
            income = (int) (400 * count * (1 + upgradeCount * 0.5));
        }
        else if (tileType.Length >=7 &&tileType.Substring(0, 7) == "chicken")
        {
            income = (int) (100 * count * (1 + upgradeCount * 0.5));
        }
        return income;
    }
}
