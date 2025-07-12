using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector2 _directionVector2;
    public Vector2 MoveDirection => _directionVector2;
    private Vector2 _angularVector = new Vector2(0.707107f, 0.707107f);

    [SerializeField]
    public float _mainSpeed { get; private set; }
    [SerializeField]
    private float _walkingSpeed = 7f;
    [SerializeField]
    private float _runSpeed = 10f;
    private bool _isMoving;
    private bool _isRunning;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        AngularVector();
    }

    public void AngularVector()
    {
        if (_directionVector2.y == _angularVector.y && _directionVector2.x == _angularVector.x)
        {
            Debug.Log(_directionVector2);
        }
        else if (_directionVector2.y == -_angularVector.y && _directionVector2.x == -_angularVector.x)
        {
            Debug.Log(_directionVector2);
        }
        else if (_directionVector2.y == -_angularVector.y && _directionVector2.x == _angularVector.x)
        {
            Debug.Log(_directionVector2);
        }
        else if (_directionVector2.y == _angularVector.y && _directionVector2.x == -_angularVector.x)
        {
            Debug.Log(_directionVector2);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            _isMoving = false;
        }
        else
        {
            _directionVector2 = direction;
            _isMoving = true;
        }
    }

    private void Move()
    {
        if (!_isMoving)
        {
            _mainSpeed = 0;
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }

        _mainSpeed = _isRunning ? _runSpeed : _walkingSpeed;
        _rigidbody2D.velocity = _directionVector2 * _mainSpeed;
    }

    public void SetRunning(bool isRunning)
    {
        _isRunning = isRunning;
    }
}
