using UnityEngine;

public class ScrapperMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public Vector2 _direction { get; private set; }

    private float _speedMain;
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
        _direction = direction.normalized;
    }

    public void SetSpeed(float speed)
    {
        _speedMain = speed;
    }

    private void Move()
    {
        _rigidbody2D.velocity = _direction * _speedMain;
    }
}
