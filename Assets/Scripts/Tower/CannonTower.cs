using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private GameObject projectilePrefab;

    /// <summary>
    /// Fires the projectile at the set enemy
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
    /// Targets the enemy that has the lowest health that is in the towers collider
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
        float lowHealth = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            Enemy enemyHealth = enemy.GetComponent<Enemy>();
            if (enemyHealth != null && enemyHealth.currentHealth < lowHealth)
            {
                lowHealth = enemyHealth.currentHealth;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
