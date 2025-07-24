using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string isWalkingBool;
    [SerializeField] private int damage;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        agent.SetDestination(endPoint.position);
        animator.SetBool(isWalkingBool, true);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                DealDamage();
            }
        }
    }

    private void DealDamage()
    {
        animator.SetBool(isWalkingBool, false);
        GameManager.Instance.playerHealth.TakeDamage(damage);
        Destroy(gameObject);
    }
}
