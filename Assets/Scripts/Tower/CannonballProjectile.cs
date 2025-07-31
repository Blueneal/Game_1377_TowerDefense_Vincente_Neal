using UnityEngine;

public class CannonballProjectile : Projectile
{
    private float knockBackForce = 5f;
    
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
