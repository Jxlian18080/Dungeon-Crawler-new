using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyNavMeshAgent : MonoBehaviour
{
    private Transform target;
    private EnemyCombat _enemyCombat;
    private EnemyHealth _enemyHealth;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _enemyCombat = GetComponent<EnemyCombat>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("PLAYER").transform;
    }

    private void Update()
    {
        if (_enemyHealth.IsDead()) return;
        SetDestination();
    }

    private void SetDestination()
    {
        if (_enemyCombat.GotHit)
        {
            StopEnemyMovement();
            StartCoroutine(WaitForGotHit());
        }

        agent.SetDestination(target.position);
    }

    public void StopEnemyMovement()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    private IEnumerator WaitForGotHit()
    {
        yield return new WaitUntil(() => _enemyCombat.GotHit == false);
        agent.isStopped = false;
    }
}
