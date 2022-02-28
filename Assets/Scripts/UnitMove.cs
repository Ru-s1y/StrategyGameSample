using UnityEngine;
using UnityEngine.AI;

public class UnitMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Vector3 goal;
    public GameObject targetMarker;

    private float dis;

    // Start is called before the first frame update
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

    public void Move(Vector3 point)
    {
        goal = point;
        agent.SetDestination(goal);
    }
}
