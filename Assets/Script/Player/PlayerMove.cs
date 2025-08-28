using System.Threading;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private StatPlayer _statPlayer;

    [SerializeField] public float MainSpeed { get; private set; }


    private Vector2 _directVector = new Vector2(0, 0);
    private Vector2 _angularVector = new Vector2(0.707107f, 0.707107f);

    private bool _isMoving;
    private bool _isRunning;
    private bool _isAngularDirection;

    public float TransitionDirect { get; private set; }
    public float TransitionAngular { get; private set; }

    public Vector2 MainDirection { get; private set; }
    public Vector2 DirectionVector2 { get; private set; }
    public Vector2 DirectionVector { get; private set; }

    

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _statPlayer = GetComponent<StatPlayer>();
    }

    private void FixedUpdate()
    {
        Move();
        AngularDirection();
        //i();

        /*if (_isMoving && _isAngularDirection)
        {
            _transitionAngular = 1;
        }*/
    }

    public void AngularDirection()
    {
        if (MainDirection.y == _angularVector.y && MainDirection.x == _angularVector.x /*&& _isMoving*/)
        {
            DirectionVector2 = MainDirection;
            _isAngularDirection = true;
            TransitionAngular = 1;
            Debug.Log("да");
        }
        else if (MainDirection.y == -_angularVector.y && MainDirection.x == -_angularVector.x /*&& _isMoving*/)
        {
            DirectionVector2 = MainDirection;
            _isAngularDirection = true;
            TransitionAngular = 1;
            Debug.Log("да");
        }
        else if (MainDirection.y == -_angularVector.y && MainDirection.x == _angularVector.x /*&& _isMoving*/)
        {
            DirectionVector2 = MainDirection;
            _isAngularDirection = true;
            TransitionAngular = 1;
            Debug.Log("да");
        }
        else if (MainDirection.y == _angularVector.y && MainDirection.x == -_angularVector.x /*&& _isMoving*/)
        {
            DirectionVector2 = MainDirection;
            _isAngularDirection = true;
            TransitionAngular = 1;
            Debug.Log("да");
        }
        else
        {
            //Thread.Sleep(1000);
            i();
        }
    }

    public void i()
    {
        if (MainDirection.y >= 1 /*&& MainDirection.x == _directVector.x*/&& _isMoving)
        {
            DirectionVector2 = MainDirection;
            TransitionAngular = 0;
            Debug.Log("нет");
        }
        else if (MainDirection.x <= 1 /*&& MainDirection.y == _directVector.y*/ && _isMoving)
        {
            DirectionVector2 = MainDirection;
            TransitionAngular = 0;
            Debug.Log("нет");
        }
        else if (MainDirection.y <= 1 /*&& MainDirection.x == _directVector.x*/ && _isMoving)
        {
            DirectionVector2 = MainDirection;
            TransitionAngular = 0;
            Debug.Log("нет");
        }
        else if (MainDirection.x >= 1 /*&& MainDirection.y == _directVector.y*/ && _isMoving)
        {
            DirectionVector2 = MainDirection;
            TransitionAngular = 0;
            Debug.Log("нет");
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
            _isMoving = true;
            MainDirection = direction;
        }
    }

    private void Move()
    {
        if (!_isMoving)
        {
            MainSpeed = 0;
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }

        MainSpeed = _isRunning ? _statPlayer.RunSpeed : _statPlayer.WalkingSpeed;
        _rigidbody2D.velocity = MainDirection * MainSpeed;
    }

    public void SetRunning(bool isRunning)
    {
        _isRunning = isRunning;
    }
}
