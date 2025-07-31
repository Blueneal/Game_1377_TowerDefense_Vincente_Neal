using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowProjectile : Projectile
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}
