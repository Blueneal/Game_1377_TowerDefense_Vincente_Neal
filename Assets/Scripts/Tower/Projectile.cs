using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 28f;
    [SerializeField] private float lifetime = 3f;
    public int damage = 5;
    public Transform target;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.forward = direction;
        }
        else
        {
            Destroy (gameObject);
        }
    }

    public void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }

    protected abstract void OnTriggerEnter(Collider other);
}
