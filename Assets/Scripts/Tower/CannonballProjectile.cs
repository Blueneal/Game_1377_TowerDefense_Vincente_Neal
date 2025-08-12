using UnityEngine;

public class CannonballProjectile : Projectile
{
    [SerializeField] private float knockBackForce = 5f;
    
    /// <summary>
    /// When the projectile hits the enemy it adds force to the object pushing it back
    /// </summary>
    /// <param name="other"></param>
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Vector3 knockback = (transform.position - enemy.transform.position).normalized;
                Rigidbody rb = enemy.GetComponent<Rigidbody>();
                rb.AddForce(knockback * knockBackForce, ForceMode.Impulse);
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
