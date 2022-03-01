using UnityEngine;
using UnityEngine.AI;

public class UnitMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Vector3 goal;
    public GameObject targetMarker;

    void Start()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
        if (targetMarker != null && agent.remainingDistance < 0.5f)
        {
            Destroy(targetMarker);
        }
    }

    //NavMeshAgentに目標地点を伝える
    public void Move(Vector3 point)
    {
        goal = point;
        agent.SetDestination(goal);
    }
}
