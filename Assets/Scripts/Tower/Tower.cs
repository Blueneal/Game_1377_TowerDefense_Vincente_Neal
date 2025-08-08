using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public float fireCoolDown = 1.0f;
    public int value;

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

    protected abstract void FireAt(Enemy Target);

    protected abstract Enemy GetClosestEnemy();
    

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
