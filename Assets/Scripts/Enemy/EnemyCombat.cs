using System.Collections;
using Retro.ThirdPersonCharacter;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private PlayerCombat _playerCombat;
    private Animator _animator;
    private EnemyHealth _enemyHealth;
    private EnemyNavMeshAgent _enemyNavMeshAgent;
    public bool GotHit { get; private set; } = false;
    private bool isAnimationOver = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyNavMeshAgent = GetComponent<EnemyNavMeshAgent>();
    }

    private void Start()
    {
        _playerCombat = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<PlayerCombat>();
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
        else if (playerWeaponTag.Equals("SWORD"))
        {
            GetComponent<Animator>().SetTrigger("SwordHit");
        }
        else if (playerWeaponTag.Equals("DAGGER"))
        {
            GetComponent<Animator>().SetTrigger("DaggerHit");
        }
    }

    private IEnumerator ResetGotHit()
    {
        yield return new WaitUntil(() => _playerCombat.AttackInProgress == false);
        GotHit = false;
    }

    public void Die()
    {
        _animator.SetInteger("DeathAnimatorIndex", Random.Range(1, 5));
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
