using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveSpeed = 100f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 newPosition = _rigidbody.position + (moveDirection * (moveSpeed * Time.fixedDeltaTime));
        _rigidbody.MovePosition(newPosition);
        Debug.Log($"Rigidbody moved to: {newPosition}");
    }
}