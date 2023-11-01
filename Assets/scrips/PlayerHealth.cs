using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHealth;

    public int currentPlayerHP;

    public int starterHP;

    public void Start()
    {
        currentPlayerHP = starterHP;
    }

    public int GetCurrentHP()
    {
        return currentPlayerHP;
    }

    public void removeHP(int amout)
    {
        currentPlayerHP -= amout;
    }

    public void Update()
    {
        if (currentPlayerHP == 0)
        {
            Application.Quit();
        }
        playerHealth.text = $"{currentPlayerHP}";
    }
}
