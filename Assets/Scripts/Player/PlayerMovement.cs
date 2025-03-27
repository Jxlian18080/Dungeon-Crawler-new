using UnityEngine;

namespace Retro.ThirdPersonCharacter
{
    public class PlayerMovement : MonoBehaviour
    {
        private Animator _animator;
        private PlayerInput _playerInput;
        private PlayerCombat _combat;
        private Rigidbody _rigidbody;

        private Vector2 lastMovementInput;
        private float moveSpeed = 10f;
        private float decelerationOnStop = 0.00f;
        private float rotationSpeed = 10f;
        private float dampTime = 0.1f;
        private Vector3 newPosition;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            _combat = GetComponent<PlayerCombat>();
            _rigidbody = GetComponent<Rigidbody>();

            _animator.applyRootMotion = false;
        }

        private void FixedUpdate()
        {
            if (_animator == null) return;

            if (_combat.AttackInProgress)
            {
                StopMovementOnAttack();
            }
            else
            {
                Move();
            }
        }

        private void Move()
        {
            var x = _playerInput.MovementInput.x;
            var y = _playerInput.MovementInput.y;
            Vector3 currentPosition = _rigidbody.position;

            Vector3 moveDirection = new Vector3(x, 0f, y);

            RotatePlayer(moveDirection);

            newPosition = currentPosition + (moveDirection * (moveSpeed * Time.fixedDeltaTime));

            _rigidbody.MovePosition(newPosition);

            _animator.SetFloat("InputX", x, dampTime, Time.deltaTime);
            _animator.SetFloat("InputY", y, dampTime, Time.deltaTime);
        }

        private void RotatePlayer(Vector3 moveDirection)
        {
            if (moveDirection.magnitude > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        private void StopMovementOnAttack()
        {
            var temp = lastMovementInput;
            temp.x -= decelerationOnStop;
            temp.y -= decelerationOnStop;
            lastMovementInput = temp;

            _animator.SetFloat("InputX", lastMovementInput.x);
            _animator.SetFloat("InputY", lastMovementInput.y);
        }
    }
}