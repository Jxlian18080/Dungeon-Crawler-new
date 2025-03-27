using System.Collections;
using Retro.ThirdPersonCharacter;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] PlayerCombat _playerCombat;
    private Animator _animator;
    private EnemyHealth _enemyHealth;
    private EnemyNavMeshAgent _enemyNavMeshAgent;
    public bool GotHit { get; private set; } = false;
    private bool isAnimationOver = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyNavMeshAgent = GetComponent<EnemyNavMeshAgent>();
    }

    public void EnemyGotHit(string playerWeaponTag, int damage)
    {
        if (GotHit) return;
        GotHit = true;
        StartCoroutine(ResetGotHit());

        _enemyHealth.TakeDamage(damage);
        if (_enemyHealth.IsDead())
        {
            Die();
            return;
        }
        if (playerWeaponTag.Equals("SWORD"))
        {
            GetComponent<Animator>().SetTrigger("Hit");
        }

    }

    private IEnumerator ResetGotHit()
    {
        yield return new WaitUntil(() => _playerCombat.AttackInProgress == false);
        GotHit = false;
    }

    public void Die()
    {
        _animator.SetInteger("DeathAnimatorIndex", Random.Range(0, 4));
        _animator.SetTrigger("Die");
        _enemyNavMeshAgent.StopEnemyMovement();
        StartCoroutine(DestroyEnemyObject());

    }

    private IEnumerator DestroyEnemyObject()
    {
        yield return new WaitUntil(() => isAnimationOver);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public void AnimationIsOver()
    {
        isAnimationOver = true;
    }
}
