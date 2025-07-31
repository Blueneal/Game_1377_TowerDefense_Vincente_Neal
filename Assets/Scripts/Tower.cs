using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float fireCoolDown = 1.0f;
    public GameObject projectilePrefab;
    protected float currentFireCoolDown = 0.0f;
    protected List<Enemy> enemiesInRange = new List<Enemy>();

    private void Update()
    {
        currentFireCoolDown -= Time.deltaTime;
        Enemy closestEnemy = GetClosestEnemy();
        if (closestEnemy != null && currentFireCoolDown <= 0.0f)
        {
            FireAt(closestEnemy);
            currentFireCoolDown = fireCoolDown;
        }
    }

    private void FireAt(Enemy Target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }

    private Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }
}
