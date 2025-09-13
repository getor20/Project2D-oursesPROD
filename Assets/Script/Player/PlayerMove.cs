using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float CurrentSpeed => _rigidbody.velocity.magnitude;

    public Vector2 MainDirection { get; private set; }
    public Vector2 DirectionVector { get; private set; }

    // Переменные для хранения последних направлений
    private Vector2 _lastAngularVector = Vector2.zero;
    private Vector2 _lastDirectVector = Vector2.zero;
    private bool _isMoving;

    // Корутина для задержки анимации
    private Coroutine _animationTransitionCoroutine;
    private float _directTransitionTimer;
    private const float _transitionDelay = 0.09f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        UpdateAnimationDirection();
    }
    private void UpdateAnimationDirection()
    {
        if (_isMoving)
        {

            // Проверяем, является ли движение строго прямым
            bool isDirect = Mathf.Approximately(MainDirection.x, 0) || Mathf.Approximately(MainDirection.y, 0);

            // Проверяем, было ли последнее движение угловым
            bool wasAngular = _lastAngularVector != Vector2.zero;

            if (isDirect)
            {
                if (wasAngular)
                {
                    // Переход с углового на прямое. Запускаем таймер.
                    _directTransitionTimer += Time.fixedDeltaTime;
                    if (_directTransitionTimer >= _transitionDelay)
                    {
                        // Таймер истек, можно переходить
                        DirectionVector = MainDirection;
                        _lastDirectVector = MainDirection;
                        _lastAngularVector = Vector2.zero;
                        _directTransitionTimer = 0; // Сбрасываем таймер
                    }
                    // Иначе ждем, сохраняя угловое состояние
                }
                else
                {
                    // Движение по прямой или старт с прямого направления. Переход без задержки.
                    _directTransitionTimer = 0;
                    DirectionVector = MainDirection;
                    _lastDirectVector = MainDirection;
                    _lastAngularVector = Vector2.zero;
                }
            }
            else
            {
                // Движение по диагонали (угловая анимация). Переход немедленный.
                _directTransitionTimer = 0; // Сбрасываем таймер
                DirectionVector = MainDirection;
                _lastAngularVector = MainDirection;
                _lastDirectVector = Vector2.zero;
            }
        }
    }

    public void Move(Vector2 direction, float speed)
    {
        MainDirection = direction;
        _rigidbody.velocity = direction.normalized * speed;
        _isMoving = direction.sqrMagnitude > 0;
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector2.zero;
        _isMoving = false;

    }

    /// <summary>
    /// Корутина для плавной задержки смены анимации при остановке.
    /// </summary>
    private IEnumerator AnimateStopTransition()
    {
        // Небольшая задержка, чтобы избежать "прыжков" анимации
        yield return new WaitForSeconds(0.05f);

        // Если игрок все еще не движется, обновляем анимацию
        if (!_isMoving)
        {
            if (_lastAngularVector != Vector2.zero)
            {
                // Игрок остановился после углового движения
                DirectionVector = _lastAngularVector;
            }
            else
            {
                // Игрок остановился после прямого движения
                DirectionVector = _lastDirectVector;
            }
        }
        _animationTransitionCoroutine = null;
    }
}
