using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float turnSpeed = 5f;
    Transform target;
    float distanceToTarget = Mathf.Infinity;
    bool onceProvoked = false;
    bool alive = true;
    NavMeshAgent navMeshAgent;
    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    void Update() {
        if (!alive) return;
        distanceToTarget = Vector3.Distance(gameObject.transform.position, target.position);
        if (distanceToTarget <= chaseRange) onceProvoked = true;
        if (onceProvoked) EngageTarget();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void EngageTarget() {
        
        if (distanceToTarget >= navMeshAgent.stoppingDistance) ChaseTarget();
        else AttackTarget();

    }

    private void ChaseTarget() {
        
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
        FaceTarget();

    }

    private void AbandonTarget() {

    }

    public void OnDamageTaken() {
        onceProvoked = true;
    }

    private void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void AttackTarget() {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    public void Die() {
        enabled = false;
        navMeshAgent.enabled = false;
    }

    private void HitPlayer() {
        print("Hit Player");
    }

}
