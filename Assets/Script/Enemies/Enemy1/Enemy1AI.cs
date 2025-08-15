using Unity.VisualScripting;
using UnityEngine;

public class Enemy1AI : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Chase
    }

    [SerializeField]
    private ConeVision _coneVision;

    [SerializeField]
    private PatrolPoint _patrolPoint;

    [SerializeField]
    private float _mainRadius;

    [SerializeField]
    private float _patrolRadius = 4f;

    [SerializeField]
    private float _chaseRadius = 6f;

    [SerializeField]
    private float _patrolPointThreshold = 0.5f;
    
    [SerializeField]
    private float distance = 3;

    [SerializeField]
    private int _indexPatrol = 0;

    private Enemy1Move _move;
    private Enemy1Animator _animator;
    public bool _isPatrol { get; private set; }

    private void Awake()
    {
        _move = GetComponent<Enemy1Move>();
        _animator = GetComponent<Enemy1Animator>();
    }

    private EnemyState _enemyState;

    private void Start()
    {
        transform.position = _patrolPoint.GetPointTransform(_indexPatrol).position;
        _enemyState = EnemyState.Patrol;
    }

    private void Update()
    {
        RunFSM();
    }

    private void SwitchState(EnemyState newState)
    {
        if (_enemyState == newState)
        {
            return;
        }

        _enemyState = newState;
    }

    private void RunFSM()
    {

        switch (_enemyState)
        {
            case EnemyState.Patrol:
                if /*радиус обзора*/ //(distanceToTarget < _patrolRadius)
                   /*угол обзора*/ (_coneVision.IsTargetInVision())
                {
                    SwitchState(EnemyState.Chase);
                }

                _isPatrol = true;
                _mainRadius = _patrolRadius;
                _move.SetPatrolSpeed();
                _coneVision.SetVisionRadius();
                ExecutePatrolState();
                
                break;
            case EnemyState.Chase:
                if /*радиус обзора*/ //(distanceToTarget > _patrolRadius)
                   /*угол обзора*/ (!_coneVision.IsTargetInVision())
                {
                    SwitchState(EnemyState.Patrol);
                }

                SetStaticState();
                _isPatrol = false;
                _mainRadius = _chaseRadius;
                _coneVision.SetVisionRadius();
                ExecuteChaseState();
                
                break;
        }
    }

    private void SetStaticState()
    {
        float currentDistance = Vector3.Distance(transform.position, _coneVision.Target.position);

        // Если расстояние меньше минимального, отталкиваемся
        if (currentDistance < distance)
        {
            // Вычисляем вектор направления от цели к нашему объекту
            Vector3 direction = (transform.position - _coneVision.Target.position).normalized;

            // Вычисляем новую позицию, отступая от цели на заданное расстояние
            transform.position = _coneVision.Target.position + direction * distance;

            _move.SetStaticSpeed();
            SetDirection(Vector2.zero);
        }
        else
        {
            _move.SetChaseSpeed();
        }

        Debug.Log(currentDistance);
    }

    private void ExecutePatrolState()
    {
        Vector2 direction = Vector2.zero;
        int randomInt = Random.Range(0, 3);
        Transform targetPoint = _patrolPoint.GetPointTransform(_indexPatrol);
        if (Vector2.Distance(transform.position, targetPoint.position) < _patrolPointThreshold)
        {
            _indexPatrol = (_indexPatrol + randomInt) % _patrolPoint.PointCount;
        }
        direction = (targetPoint.position - transform.position).normalized;

        if (direction != Vector2.zero)
        {
            SetDirection(direction);
        }
    }

    private void ExecuteChaseState()
    {
        Vector2 direction = (_coneVision.Target.position - transform.position).normalized;
        SetDirection(direction);
    }

    private void SetDirection(Vector2 direction)
    {
        _move.SetMoveDirection(direction);
        _animator.SetDirection(direction);
        _coneVision.SetDirection(direction);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _mainRadius);
    }
}