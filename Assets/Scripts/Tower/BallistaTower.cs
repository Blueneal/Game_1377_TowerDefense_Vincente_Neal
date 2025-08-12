using UnityEngine;

public class BallistaTower : Tower
{
    [SerializeField] private GameObject projectilePrefab;

    /// <summary>
    /// Fires arrow projectile at the first enemy in the list
    /// </summary>
    /// <param name="Target"></param>
    protected override void FireAt(Enemy Target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(Target.transform);
        }
    }

    /// <summary>
    /// Finds the closest enemy in the list of enemies and sets that as the towers target
    /// </summary>
    /// <returns></returns>
    protected override Enemy GetClosestEnemy()
    {
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (enemiesInRange[i] == null)
            {
                enemiesInRange.RemoveAt(i);
            }
        }
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
}
