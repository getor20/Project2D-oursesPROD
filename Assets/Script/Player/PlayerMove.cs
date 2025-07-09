using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector2 directionVector2;
    public Vector2 moveDirection => directionVector2;

    [SerializeField]
    private float _mainSpeed;
    [SerializeField]
    private float _walkingSpeed = 7f;
    [SerializeField]
    private float _runSpeed = 10f;
    private bool _isMoving;
    public bool IsMove => _isMoving;
    private bool _isRunning;

    private void Start()
    {
        _mainSpeed = _walkingSpeed;
    }
        
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            _isMoving = false;
        }
        else
        {
            directionVector2 = direction;
            _isMoving = true;
        }
    }

    private void Move()
    {
        if (!_isMoving)
        {
            rigidbody2D.velocity = Vector2.zero;
            return;
        }

        _mainSpeed = _isRunning ? _runSpeed : _walkingSpeed;
        rigidbody2D.velocity = directionVector2 * _mainSpeed;
    }

    public void SetRunning(bool isRunning)
    {
        _isRunning = isRunning;
    }
}
