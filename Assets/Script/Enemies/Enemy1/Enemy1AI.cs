using UnityEngine;

public class Enemy1AI : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Pursuit
    }

    [SerializeField]
    private float _mainRadius;
    [SerializeField]
    private float _patrolRadius = 3;
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
        _mainRadius = _patrolRadius;
        _enemyState = EnemyState.Patrol;
        _move.SetMoveDirection(Vector2.zero); // ”бедитесь, что он начинает движение с остановки
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

        // ќбработка выхода из текущего состо€ни€ (необ€зательно, но хорошо дл€ очистки)
        // ƒл€ ваших текущих состо€ний никаких специальных действий при выходе не требуетс€

        _enemyState = newState;

        // ќбработка входа в новое состо€ние
        if (_enemyState == EnemyState.Patrol)
        {
            Debug.Log("патруль");
            _mainRadius = _patrolRadius;
            _move.SetMoveDirection(Vector2.zero); // ќстанавливаем движение при входе в Patrol
            // ¬ы можете добавить здесь специфическую логику патрулировани€, например, движение к путевым точкам
        }
        else if (_enemyState == EnemyState.Pursuit)
        {
            Debug.Log("ѕреследовани€");
            _mainRadius = _pursuitRadius;
            // Ќикаких специальных действий при входе в Pursuit не требуетс€, так как это обрабатываетс€ в ExecuteChaseState
        }
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
                // «десь нет кода движени€, он обрабатываетс€ SwitchState, устанавливающим направление в ноль
                break;
            case EnemyState.Pursuit:
                if (distanceToTarget > _mainRadius)
                {
                    SwitchState(EnemyState.Patrol);
                }
                else
                {
                    ExecuteChaseState(); // ѕродолжаем преследование, если все еще в радиусе преследовани€
                }
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
        Gizmos.DrawWireSphere(transform.position, _mainRadius);
    }
}