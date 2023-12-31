using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenShots;

    private float nextTimeToShoot;

    public GameObject currentTarget;

    private void Start()
    {
        nextTimeToShoot = Time.time;
    }

    private void updateNearestEnemy()
    {
        GameObject currentnearestenemy = null;

        float distance = Mathf.Infinity;

        foreach(GameObject enemy in Enemies.enemies)
        {
            if (enemy != null)
            {
                float _distance = (transform.position - enemy.transform.position).magnitude;

                if (_distance < distance)
                {
                    distance = _distance;
                    currentnearestenemy = enemy;
                }
            }
        }

        if (distance <= range)
        {
            currentTarget = currentnearestenemy;
        }
        else
        {
            currentTarget = null;
        }
    }

    protected virtual void shoot()
    {
        Enemy enemyScript = currentTarget.GetComponent<Enemy>();

        enemyScript.takeDamage(damage);
    }

    private void Update()
    {
        updateNearestEnemy();

        if (Time.time >= nextTimeToShoot)
        {
            if(currentTarget != null)
            {
                shoot();
                nextTimeToShoot = Time.time + timeBetweenShots;
            }
        }
    }
}
