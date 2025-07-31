using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
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
        animator.SetBool(isWalkingBool, true);
    }

    public void Initialize(Transform inputEndPoint)
    {
        endPoint = inputEndPoint;
        agent.SetDestination(endPoint.position);
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
