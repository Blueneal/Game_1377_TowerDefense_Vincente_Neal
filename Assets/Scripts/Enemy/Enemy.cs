using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string isWalkingBool;
    [SerializeField] private int damage;
    public int maxHealth = 20;
    public int currentHealth;
    public int value;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Checks to see if the enemy has the tag of horse, and sets the animator in the child
    /// Sets the animator for enemies
    /// </summary>
    void Start()
    {
        if (gameObject.CompareTag("Horse"))
        {
            animator = GetComponentInChildren<Animator>();
            animator.SetBool(isWalkingBool, true);
        }
        animator.SetBool(isWalkingBool, true);
    }

    /// <summary>
    /// Sets the destination of the enemy by what endpoint was set in WaveManager and the editor
    /// </summary>
    /// <param name="inputEndPoint"></param>
    public void Initialize(Transform inputEndPoint)
    {
        endPoint = inputEndPoint;
        agent.SetDestination(endPoint.position);
    }

    /// <summary>
    /// checks to see if the enemy is at 0 health and removes the game object, and adds the value of the enemy to the players current money
    /// </summary>
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            TowerPlaceManager towerManager = FindAnyObjectByType<TowerPlaceManager>();
            towerManager.AddMoney(value);
            GameManager.Instance.waveManager.enemiesInLevel--;
        }
    }

    /// <summary>
    /// Deals damage to the player when the enemy enters the end point
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            DealDamage();
        }
    }

    /// <summary>
    /// Deals the set amount of damage that the enemy is assigned in the editor
    /// </summary>
    private void DealDamage()
    {
        animator.SetBool(isWalkingBool, false);
        GameManager.Instance.playerHealth.TakeDamage(damage);
        Destroy(gameObject);
        GameManager.Instance.waveManager.enemiesInLevel--;
    }

    /// <summary>
    /// Removes from the enemy health based off the damage value from the projectile
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);
    }
}
