using UnityEngine;
using UnityEngine.AI;

public class CrystalProjectile : Projectile
{
    public int slowDown;

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
