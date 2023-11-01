using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Moneymanager moneymanager;
    PlayerHealth playerhealth;

    [SerializeField]
    private float enemyHealth;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private int damage;


    private GameObject targetTile;

    private void Awake()
    {
        Enemies.enemies.Add(gameObject);
    }

    private void Start()
    {
        initializeEnemy();
        moneymanager = FindObjectOfType<Moneymanager>();
        playerhealth = FindObjectOfType<PlayerHealth>();
    }

    private void initializeEnemy()
    {
        targetTile = MapGenerator.startTile;
    }

    public void takeDamage(float amout)
    {
        enemyHealth -= amout;

        if (enemyHealth <= 0)
        {
            die();
        }
    }

    private void die()
    {
        moneymanager.addMoney(100);
        Enemies.enemies.Remove(gameObject);
        Destroy(transform.gameObject);
    }

    private void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position, movementSpeed * Time.deltaTime);
    }

    private void checkPosition()
    {
        if (targetTile != null && targetTile != MapGenerator.endTile)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;

            if (distance < 0.001f)
            {
                int currentIndex = MapGenerator.pathTiles.IndexOf(targetTile);

                targetTile = MapGenerator.pathTiles[currentIndex + 1];
            }
        }
    }

    private void Update()
    {
        if (transform.position == MapGenerator.endTile.transform.position)
        {
            playerhealth.removeHP(damage);
            Enemies.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
        checkPosition();
        moveEnemy();

        takeDamage(0);
    }
}
