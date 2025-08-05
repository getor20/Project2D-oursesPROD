using UnityEngine;

public class Enemy1AI : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Pursuit
    }

    [SerializeField]
    private Transform[] _waypoints;
    [SerializeField]
    private float _mainRadius;
    [SerializeField]
    private float _patrolRadius = 3;
    [SerializeField]
    private float _pursuitRadius = 6;
    [SerializeField]
    private float _patrolPointThreshold = 0.5f;
    [SerializeField]
    private int _indexMassif = 0;
    public bool _isPatrol { get; private set; }
    [SerializeField]
    private Transform _target;
    private Enemy1Move _move;
    private Enemy1Animator _animator;

    private void Awake()
    {
        _move = GetComponent<Enemy1Move>();
        _animator = GetComponent<Enemy1Animator>();
    }

    private EnemyState _enemyState;

    private void Start()
    {
        transform.position = _waypoints[0].position;
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
        float distanceToTarget = Vector2.Distance(transform.position, _target.position);

        switch (_enemyState)
        {
            case EnemyState.Patrol:
                if (distanceToTarget < _mainRadius)
                {
                    SwitchState(EnemyState.Pursuit);
                }
                _isPatrol = true;
                _mainRadius = _patrolRadius;
                ExecutePatrolState();
                break;
            case EnemyState.Pursuit:
                if (distanceToTarget > _mainRadius)
                {
                    SwitchState(EnemyState.Patrol);
                }
                _isPatrol = false;
                _mainRadius = _pursuitRadius;
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
            targetPoint = _waypoints[_indexMassif];
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
        Vector2 direction = (_target.position - transform.position).normalized;
        _move.SetMoveDirection(direction);
        _animator.SetDirection(direction);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _mainRadius);
    }
}