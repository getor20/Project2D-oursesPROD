using UnityEngine;

public class Enemy1Move : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _directionVector2;

    [SerializeField]
    private float _mainSpeed;
    [SerializeField]
    private float _speedPatrol = 2;
    [SerializeField]
    private float _speedPursuit = 5;
    [SerializeField]

    public float CurrentSpeed => _rigidbody2D.velocity.magnitude;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _mainSpeed = _speedPatrol;
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
