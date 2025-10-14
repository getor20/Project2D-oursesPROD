using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _timerDelay; // Задержка таймера

    private Rigidbody2D _rigidbody;

    private Vector2 _mainDirection;
    private Vector2 _angularVector = Vector2.zero;

    private float _timer;
    
    public Vector2 DirectionVector { get; private set; }
    public float CurrentSpeed => _rigidbody.velocity.magnitude;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        UpdateDirectionVector();
    }

    private void UpdateDirectionVector()
    {
        if (CurrentSpeed > 0)
        {
            //  движение прямым
            bool isDirect = Mathf.Approximately(_mainDirection.x, 0) || Mathf.Approximately(_mainDirection.y, 0);

            //  движение угловым
            bool isAngular = _angularVector != Vector2.zero;

            if (isDirect)
            {
                if (isAngular)
                {
                    // Переход с углового на прямое. Запуск таймер.
                    _timer += Time.fixedDeltaTime;
                    if (_timer >= _timerDelay)
                    {
                        // переход
                        DirectionVector = _mainDirection;
                        _angularVector = Vector2.zero;
                        _timer = 0; // Обнуляем таймер
                    }
                }
                else
                {
                    // Движение по прямой
                    _timer = 0;
                    DirectionVector = _mainDirection;
                    _angularVector = Vector2.zero;
                }
            }
            else
            {
                // Движение по диагонали
                _timer = 0; // Обнуляем таймер
                DirectionVector = _mainDirection;
                _angularVector = _mainDirection;
            }
        }
    }

    public void Move(Vector2 direction, float speed)
    {
        _mainDirection = direction;
        _rigidbody.velocity = direction.normalized * speed;
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
    }
}
