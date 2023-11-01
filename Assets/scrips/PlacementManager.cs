using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public ShopManager shopManager;

    private GameObject currentTowerPlacing;

    private GameObject dummyPlacement;

    private GameObject hoverTile;

    public Camera cam;

    public LayerMask mask;
    public LayerMask towerMask;

    public bool isBuilding;

    public void Start()
    {

    }

    public Vector2 getMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void getCurrentHoverTile()
    {
        Vector2 mousePosition = getMousePosition();

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, mask, -100, 100);

        if (hit.collider != null)
        {
            if (MapGenerator.mapTiles.Contains(hit.collider.gameObject))
            {
                if (!MapGenerator.pathTiles.Contains(hit.collider.gameObject))
                {
                    hoverTile = hit.collider.gameObject;
                }
            }
        }
    }

    public bool checkForTower()
    {
        bool towerOnSlot = false;

        Vector2 mousePosition = getMousePosition();

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, towerMask, -100, 100);

        if (hit.collider != null)
        {
            towerOnSlot = true;
        }

        return towerOnSlot;
    }

    public void placeBuilding()
    {
        if (hoverTile != null)
        {
            if(checkForTower() == false)
            {
                if (shopManager.canBuyTower(currentTowerPlacing) == true)
                {
                    GameObject newTowerObject = Instantiate(currentTowerPlacing);
                    newTowerObject.layer = LayerMask.NameToLayer("Tower");
                    newTowerObject.transform.position = hoverTile.transform.position;

                    endBuilding();
                    shopManager.buyTower(currentTowerPlacing);
                }
                else
                {
                    Debug.Log("not enough money!");
                }
            }
        }
    }

    public void startBuilding(GameObject towerToBuild)
    {
        isBuilding = true;

        currentTowerPlacing = towerToBuild;

        dummyPlacement = Instantiate(currentTowerPlacing);

        if (dummyPlacement.GetComponent<Tower>() != null)
        {
            Destroy(dummyPlacement.GetComponent<Tower>());
        }

        if (dummyPlacement.GetComponent<BarrelRotation>() != null)
        {
            Destroy(dummyPlacement.GetComponent<BarrelRotation>());
        }
    }

    public void endBuilding()
    {
        isBuilding = false;

        if (dummyPlacement != null)
        {
            Destroy(dummyPlacement);
        }
    }

    public void Update()
    {
        if (isBuilding == true)
        {
            if (dummyPlacement != null)
            {
                getCurrentHoverTile();
                
                if (hoverTile != null)
                {
                    dummyPlacement.transform.position = hoverTile.transform.position;
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                placeBuilding();
            }
        }
    }
}
