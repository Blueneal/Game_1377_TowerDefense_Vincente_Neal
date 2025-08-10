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

    void Start()
    {
        if (gameObject.CompareTag("Horse"))
        {
            animator = GetComponentInChildren<Animator>();
            animator.SetBool(isWalkingBool, true);
        }
        animator.SetBool(isWalkingBool, true);
    }

    public void Initialize(Transform inputEndPoint)
    {
        endPoint = inputEndPoint;
        agent.SetDestination(endPoint.position);
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            DealDamage();
        }
    }

    private void DealDamage()
    {
        animator.SetBool(isWalkingBool, false);
        GameManager.Instance.playerHealth.TakeDamage(damage);
        Destroy(gameObject);
        GameManager.Instance.waveManager.enemiesInLevel--;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);
    }
}
