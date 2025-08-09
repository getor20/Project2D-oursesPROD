using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    public float _mainSpeed { get; private set; }

    [SerializeField]
    private float _walkingSpeed = 7f;

    [SerializeField]
    private float _runSpeed = 9f;

    private Vector2 _directVector = new Vector2(0, 0);
    private Vector2 _angularVector = new Vector2(0.707107f, 0.707107f);

    private bool _isMoving;
    private bool _isRunning;
    private bool _isAngularDirection;

    public float _transitionDirect { get; private set; }
    public float _transitionAngular { get; private set; }

    public Vector2 _directionVector2 { get; private set; }
    public Vector2 _directionVector { get; private set; }

    

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        AngularDirection();
        //DirectVector();
        //Debug.Log("AngularDirection: " + _isAngularDirection);
        //Debug.Log(_directionVector2);
        //Debug.Log("_isMoving: " + _isMoving);
        if (!_isMoving && _isAngularDirection)
        {
            _transitionAngular = 1;
            //Debug.Log("угол да");
        }
    }

    public void DirectVector()
    {
        if (_directionVector2.y == _directVector.y && _directionVector2.x > _directVector.x)
        {

        }
        else if (_directionVector2.y == _directVector.y && _directionVector2.x < _directVector.x)
        {

        }
        else if (_directionVector2.y > _directVector.y && _directionVector2.x == _directVector.x)
        {

        }
        else if (_directionVector2.y < _directVector.y && _directionVector2.x == _directVector.x)
        {

        }

    }

    public void AngularDirection()
    {
        if (_directionVector2.y == _angularVector.y && _directionVector2.x == _angularVector.x)
        {
            _isAngularDirection = true;
            _directionVector = _directionVector2;
            _transitionAngular = 1;
            //Debug.Log("_isAngularDirection: " + _isAngularDirection);
            //Debug.Log("_vector1" + _directionVector2);
        }
        else if (_directionVector2.y == -_angularVector.y && _directionVector2.x == -_angularVector.x)
        {
            _isAngularDirection = true;
            _directionVector = _directionVector2;
            _transitionAngular = 1;
            //Debug.Log("_isAngularDirection: " + _isAngularDirection);
            //Debug.Log("_vector2" + _directionVector2);
        }
        else if (_directionVector2.y == -_angularVector.y && _directionVector2.x == _angularVector.x)
        {
            _isAngularDirection = true;
            _directionVector = _directionVector2;
            _transitionAngular = 1;
            //Debug.Log("_isAngularDirection: " + _isAngularDirection);
            //Debug.Log("_vector3" + _directionVector2);
        }
        else if (_directionVector2.y == _angularVector.y && _directionVector2.x == -_angularVector.x)
        {
            _isAngularDirection = true;
            _directionVector = _directionVector2;
            _transitionAngular = 1;
            //Debug.Log("_isAngularDirection: " + _isAngularDirection);
            //Debug.Log("_vector4" + _directionVector2);
        }
        else
        {
            _transitionAngular = 0;
            _isAngularDirection = false;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
        { 
            _isMoving = false;
            if (_directionVector2 == Vector2.up || _directionVector2 == Vector2.down || _directionVector2 == Vector2.right || _directionVector2 == Vector2.left)
            {
                _transitionAngular = 0;
                //Debug.Log("На верх");
            }
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
