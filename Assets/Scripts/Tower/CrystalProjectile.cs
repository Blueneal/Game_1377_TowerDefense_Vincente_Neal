using UnityEngine;
using UnityEngine.AI;

public class CrystalProjectile : Projectile
{
    public int slowDown;

    /// <summary>
    /// Slows down the enemy set by the integer set in the editor before deleting the prefab
    /// </summary>
    /// <param name="other"></param>
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
                agent.speed = slowDown;
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
