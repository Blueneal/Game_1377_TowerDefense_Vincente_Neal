using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public float fireCoolDown = 1.0f;
    public int value;

    protected float currentFireCoolDown = 0.0f;
    protected List<Enemy> enemiesInRange = new List<Enemy>();

    /// <summary>
    /// checks every frame for an enemy that is added to the closest enemy list, and then activates the FireAt Function
    /// </summary>
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

    /// <summary>
    /// Two following functions are abstracted for ease of use across different towers
    /// </summary>
    /// <param name="Target"></param>
    protected abstract void FireAt(Enemy Target);

    protected abstract Enemy GetClosestEnemy();
    
    /// <summary>
    /// Looks for an enemy that enters the tower objects collider who then is added to the EnemisInRange List
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    /// <summary>
    /// Checks to see if an enemy exits the towers collider, and then removes the enemy 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }
}
