using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] protected Collider weaponCollider;
    [SerializeField] private int damage;

    private void Start() {
        DisableCollider();
    }

    private void Update() {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ENEMY"))
        {
            other.GetComponent<EnemyCombat>().EnemyGotHit(tag, damage);
        }
    }

    public void EnableCollider() { 
        weaponCollider.enabled = true; 
    }

    public void DisableCollider() { 
        weaponCollider.enabled = false;
    }
}
