using UnityEngine;

public class Enemy1AI : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Chase
    }

    [SerializeField]
    private Transform[] _waypoints;

    [SerializeField]
    private ConeVision _coneVision;

    [SerializeField]
    private float _mainRadius;

    [SerializeField]
    private float _patrolRadius = 4f;

    [SerializeField]
    private float _chaseRadius = 6f;

    [SerializeField]
    private float _patrolPointThreshold = 0.5f;

    [SerializeField]
    private int _indexMassif = 0;

    private Enemy1Move _move;
    private Enemy1Animator _animator;
    public bool _isPatrol { get; private set; }

    public Vector2 Position;

    private void Awake()
    {
        _move = GetComponent<Enemy1Move>();
        _animator = GetComponent<Enemy1Animator>();
    }

    private EnemyState _enemyState;

    private void Start()
    {
        transform.position = _waypoints[_indexMassif].position;
        _enemyState = EnemyState.Patrol;
    }

    private void Update()
    {
        Debug.Log(_coneVision.IsTargetInVision());
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
        float distanceToTarget = Vector2.Distance(transform.position, _coneVision.Target.position);

        switch (_enemyState)
        {
            case EnemyState.Patrol:
                if /*נאהטףס מבחמנא*/ //(distanceToTarget < _patrolRadius)
                   /*ףדמכ מבחמנא*/ (_coneVision.IsTargetInVision())
                {
                    SwitchState(EnemyState.Chase);
                }
                _isPatrol = true;
                _mainRadius = _patrolRadius;
                _move.SetPatrolSpeed();
                ExecutePatrolState();
                break;
            case EnemyState.Chase:
                if /*נאהטףס מבחמנא*/ //(distanceToTarget > _patrolRadius)
                   /*ףדמכ מבחמנא*/ (!_coneVision.IsTargetInVision())
                {
                    SwitchState(EnemyState.Patrol);
                }
                _isPatrol = false;
                _move.SetChaseSpeed();
                _mainRadius = _chaseRadius;
                ExecuteChaseState();
                
                break;
        }
    }

    private void ExecutePatrolState()
    {
        Vector2 direction = Vector2.zero;
        int randomInt = Random.Range(0, 3);
        Transform targetPoint = _waypoints[_indexMassif];
        if (Vector2.Distance(transform.position, targetPoint.position) < _patrolPointThreshold)
        {
            _indexMassif = (_indexMassif + randomInt) % _waypoints.Length;
        }
        direction = (targetPoint.position - transform.position).normalized;

        if (direction != Vector2.zero)
        {
            _move.SetMoveDirection(direction);
            _animator.SetDirection(direction);
        }
    }

    private void ExecuteChaseState()
    {
        Vector2 direction = (_coneVision.Target.position - transform.position).normalized;
        _move.SetMoveDirection(direction);
        _animator.SetDirection(direction);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _mainRadius);
    }
}