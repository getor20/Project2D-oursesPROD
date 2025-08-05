using UnityEngine;

public class Enemy1Move : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _directionVector2;
    private Enemy1AI _enemyAI;

    [SerializeField]
    private float _mainSpeed;
    [SerializeField]
    private float _speedPatrol = 2f;
    [SerializeField]
    private float _speedPursuit = 7f;
    [SerializeField]

    public float CurrentSpeed => _rigidbody2D.velocity.magnitude;

    private void Awake()
    {
        _enemyAI = GetComponent<Enemy1AI>();
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
        _mainSpeed = _enemyAI._isPatrol ? _speedPatrol : _speedPursuit;
        _rigidbody2D.velocity = _directionVector2 * _mainSpeed;
    }
}
