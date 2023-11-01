using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Moneymanager moneyManager;

    public GameObject basicTowerPrefab;
    public GameObject fastTowerPrefab;

    public int basicTowerCost;
    public int fastTowercost;

    public int getTowerCost(GameObject towerPrefab)
    {
        int cost = 0;

        if (towerPrefab == basicTowerPrefab)
        {
            cost = basicTowerCost;
        }
        if (towerPrefab == fastTowerPrefab)
        {
            cost = fastTowercost;
        }

        return cost;
    }

    public void buyTower(GameObject towerPrefab)
    {
        moneyManager.removeMoney(getTowerCost(towerPrefab));
    }

    public bool canBuyTower(GameObject towerPrefab)
    {
        int cost = getTowerCost(towerPrefab);

        bool canBuy = false;

        if(moneyManager.GetCurrentMoney() >= cost)
        {
            canBuy = true;
        }

        return canBuy;
    }
}
