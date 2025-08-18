using UnityEngine;

public class Enemy1Move : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _directionVector2;

    [SerializeField] private float _speedMain;
    [SerializeField] private float _speedPatrol = 2f;
    [SerializeField] private float _speedChase = 7f;
    
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

    public void SetChaseSpeed()
    {
        _speedMain = _speedChase;
    }

    public void SetPatrolSpeed()
    {
        _speedMain = _speedPatrol;
    }

    public void SetStaticSpeed()
    {
        Debug.Log("SetStaticSpeed called");
        _speedMain = 0;
    }

    private void Move()
    {
        _rigidbody2D.velocity = _directionVector2 * _speedMain;
    }
}
