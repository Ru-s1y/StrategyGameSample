using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class UnitMove : MonoBehaviour
{
    private BuildNavMesh buildNavMesh;
    private BattleManager battleManager;
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
        buildNavMesh  = GameObject.Find("Earth").GetComponent<BuildNavMesh>();
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    void Update()
    {
        if (die) return;
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);

        if (agent.remainingDistance > distance) return;

        agent.isStopped = true;
        if (target == null) {
            animator.SetBool("Attack", false);
            Destroy(targetMarker);
        } else {
            transform.LookAt(target.transform);
            Attack();
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
            return;
        animator.SetBool("Attack", true);
    }

    public void HitAttack()
    {
        if (target == null) {
            animator.SetBool("Attack", false);
            return;
        }

        if (target.gameObject.tag == "Enemy")
            battleManager.AttackUnit(this.gameObject, target);
        else
            battleManager.AttackObject(this.gameObject, target);
    }

    // 死んだアニメーション
    public void Die()
    {
        die = true;
        animator.SetBool("Die", true);
        Destroy(this.gameObject);
        buildNavMesh.RebuildNavMesh();
    }
}
