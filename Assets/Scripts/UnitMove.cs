using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class UnitMove : MonoBehaviour
{
    private NavMeshSurface navSur;
    private NavMeshAgent agent;
    private Animator animator;
    public Vector3 goal;
    public GameObject target;
    public GameObject targetMarker;
    public float distance = 0.5f;

    public bool die = false;

    void Start()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navSur   = GameObject.Find("Earth").GetComponent<NavMeshSurface>();
    }

    void Update()
    {
        if (die) return;
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
        if (agent.remainingDistance < distance)
        {
            agent.isStopped = true;
            if (target == null) {
                animator.SetBool("Attack", false);
                Destroy(targetMarker);
            } else {
                transform.LookAt(target.transform);
                Attack();
            }
        }
    }

    //NavMeshAgentに目標地点を伝える
    public void Move(Vector3 point)
    {
        if (die) return; //死んでる場合は処理とばす
        animator.SetBool("Attack", false);
        goal = point;
        agent.SetDestination(goal);
        agent.isStopped = false;
    }

    public void Attack()
    {
        if (die || target == null)
        {
            Debug.Log("Target is Empty.");
            return;
        }
        animator.SetBool("Attack", true);
    }

    public void HitAttack()
    {
        // ターゲットがユニットかオブジェクトかの判定
        float atk = GetComponent<UnitStatus>().status.atk;
        target.GetComponent<ObjectStatus>().DecreaseStatus("hp", atk);
        if (target == null)
            animator.SetBool("Attack", false);
        Debug.Log(target.GetComponent<ObjectStatus>().status.hp);
    }

    // 死んだアニメーション
    public void Die()
    {
        die = true;
        animator.SetBool("Die", true);
        Destroy(this.gameObject);
        StartCoroutine("RebuildNavMesh");
    }

    // お前はもう死んでいる!!!
    IEnumerator RebuildNavMesh()
    {
        navSur.BuildNavMesh();
        yield return new WaitForSeconds(1f);
    }
}
