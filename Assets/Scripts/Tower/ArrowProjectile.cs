using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowProjectile : Projectile
{
    /// <summary>
    /// Hits the enemy for a set amount of damage before the arrow is deleted from the scene
    /// </summary>
    /// <param name="other"></param>
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
