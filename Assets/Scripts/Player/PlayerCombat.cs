using UnityEngine;
using NaughtyCharacter;

namespace Retro.ThirdPersonCharacter
{
    public class PlayerCombat : MonoBehaviour
    {
        private const string attackTriggerName = "Attack";
        private const string specialAttackTriggerName = "Ability";

        [SerializeField] private PlayerWeapon[] _playerWeapons;
        private PlayerWeapon _playerWeapon;

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
                Attack(attackTriggerName, 0);
            }
            else if (_playerInput.SpecialAttackInput && !AttackInProgress)
            {
                Attack(specialAttackTriggerName, 1);
            }
        }
        
        private void Attack(string attackTriggerName, short weaponIndex)
        {
            _animator.SetTrigger(attackTriggerName);
            SetAttackWeapon(weaponIndex);
        }

        private void SetAttackWeapon(short weapon) 
        {
            _playerWeapon = _playerWeapons[weapon];
        }

        private void SetAttackStart()
        {
            AttackInProgress = true;
            _playerWeapon.EnableCollider();
        }

        private void SetAttackEnd()
        {
            AttackInProgress = false;
            _playerWeapon.DisableCollider();
        }
    }
}
