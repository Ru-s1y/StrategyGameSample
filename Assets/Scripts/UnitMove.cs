using UnityEngine;
using UnityEngine.AI;

public class UnitMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Vector3 goal;
    public GameObject targetMarker;
    public float distance = 0.5f;

    void Start()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
        if (targetMarker != null && agent.remainingDistance < distance)
        {
            agent.isStopped = true;
            Destroy(targetMarker);
        }
    }

    //NavMeshAgentに目標地点を伝える
    public void Move(Vector3 point)
    {
        goal = point;
        agent.SetDestination(goal);
        agent.isStopped = false;
    }
}
