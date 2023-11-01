using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Moneymanager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerMoney;

    public int currentPlayerMoney;

    public int starterMoney;

    public void Start()
    {
        currentPlayerMoney = starterMoney;
    }

    public int GetCurrentMoney()
    {
        return currentPlayerMoney;
    }

    public void addMoney(int amout)
    {
        currentPlayerMoney += amout;
    }

    public void removeMoney(int amout)
    {
        currentPlayerMoney -= amout;
        Debug.Log("removed" + amout + " from player's money! the player now has " + currentPlayerMoney);
    }

    public void Update()
    {
        playerMoney.text = $"{currentPlayerMoney}";
    }
}
