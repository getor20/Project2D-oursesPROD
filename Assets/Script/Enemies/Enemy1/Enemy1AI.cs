using UnityEngine;

public class Enemy1AI : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Pursuit
    }

    [SerializeField]
    private float _pursuitRadius = 5;
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

        switch(_enemyState)
        {
            case EnemyState.Patrol:
                if (distanceToTarget < _pursuitRadius)
                {
                    SwitchState(EnemyState.Pursuit);
                }
                break;
            case EnemyState.Pursuit:
                if (distanceToTarget > _pursuitRadius)
                {
                    SwitchState(EnemyState.Patrol);
                }
                ExecuteChaseState();
                break;
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
        Gizmos.DrawWireSphere(transform.position, _pursuitRadius);
    }
}
