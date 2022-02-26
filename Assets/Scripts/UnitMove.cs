using UnityEngine;
using UnityEngine.AI;

public class UnitMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Vector3 goal;

    // Start is called before the first frame update
    void Start()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }

    public void Move()
    {
        agent.SetDestination(goal);
    }
}
