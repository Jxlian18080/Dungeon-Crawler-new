using UnityEngine;
using NaughtyCharacter;
using System.Collections;

namespace Retro.ThirdPersonCharacter
{
    public class PlayerCombat : MonoBehaviour
    {
        private const string attackTriggerName = "Attack";
        private const string specialAttackTriggerName = "Ability";

        [SerializeField] private PlayerWeapon[] _playerWeapons;
        private PlayerWeapon attackWeapon;

        private Animator _animator;
        private PlayerInput _playerInput;
        

        public bool AttackInProgress {get; private set;} = false;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (_playerInput.AttackInput && !AttackInProgress)
            {
                StartCoroutine(Attack(attackTriggerName, 0, 0.2f));
            }
            else if (_playerInput.SpecialAttackInput && !AttackInProgress)
            {
                StartCoroutine(Attack(specialAttackTriggerName, 1, 1f));
            }
        }
        
        private IEnumerator Attack(string attackTriggerName, short weaponIndex, float delay)
        {
            _animator.SetTrigger(attackTriggerName);
            yield return new WaitForSeconds(delay);
            attackWeapon = _playerWeapons[weaponIndex];
            attackWeapon.EnableCollider();
        }

        private void SetAttackStart()
        {
            AttackInProgress = true;
        }

        private void SetAttackEnd()
        {
            AttackInProgress = false;
            attackWeapon.DisableCollider();
        }
    }
}
