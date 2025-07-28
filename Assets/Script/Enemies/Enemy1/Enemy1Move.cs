using UnityEngine;

public class Enemy1Move : MonoBehaviour
{
    [SerializeField]
    private float _mainSpeed = 5;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _directionVector2;

    public float CurrentSpeed => _rigidbody2D.velocity.magnitude;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetMoveDirection(Vector2 direction)
    {
        _directionVector2 = direction.normalized;
    }

    private void Move()
    {
        _rigidbody2D.velocity = _directionVector2 * _mainSpeed;
    }
}
