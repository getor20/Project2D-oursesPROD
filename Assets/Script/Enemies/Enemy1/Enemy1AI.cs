using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Enemy1AI : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Chase
    }

    [SerializeField] private ConeVision _coneVision;
    [SerializeField] private PatrolPath _patrolPath;

    [SerializeField] private float _patrolPointThreshold = 0.5f;
    [SerializeField] private int _indexPatrol = 0;
    [SerializeField] private float _attackDistance = 3;
    [SerializeField] private float _waitTime = 0f;

    private Enemy1Move _move;
    private Enemy1Animator _animator;

    private EnemyState _enemyState;

    private void Awake()
    {
        _move = GetComponent<Enemy1Move>();
        _animator = GetComponent<Enemy1Animator>();
    }

    private void Start()
    {
        if (_patrolPath is not null && _patrolPath.Length > 1)
        {
           try
            {
                transform.position = _patrolPath.GetPoint(_indexPatrol).Position;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.LogError($"Стартовый индекс {_indexPatrol} некорректен: {ex.Message}. Позиция врага ");
                return;
            }
        }

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
                if (_coneVision.IsTargetInVision())
                {
                    SwitchState(EnemyState.Chase);
                }
                ExecutePatrolState();
                break;
            case EnemyState.Chase:
                if (!_coneVision.IsTargetInVision())
                {
                    SwitchState(EnemyState.Patrol);
                }
                ExecuteChaseState();
                break;
        }
    }

    /*private void SetStaticState()
    {
        float currentDistance = Vector3.Distance(transform.position, _coneVision.Target.position);

        // Если расстояние меньше минимального, отталкиваемся
        if (currentDistance < _attackDistance)
        {
            // Вычисляем вектор направления от цели к нашему объекту
            Vector3 direction = (transform.position - _coneVision.Target.position).normalized;

            // Вычисляем новую позицию, отступая от цели на заданное расстояние
            transform.position = _coneVision.Target.position + direction * _attackDistance;

            _move.SetStaticSpeed();
            SetDirection(Vector2.zero);
        }
        else
        {
            _move.SetChaseSpeed();
        }

        Debug.Log(currentDistance);
    }*/

    private void ExecutePatrolState()
    {
        if (_patrolPath is null || _patrolPath.Length <= 1)
        {
            return;
        }

        PatrolPoint currentPoint = _patrolPath.GetPoint(_indexPatrol);
        Vector2 direction = (currentPoint.Position - (Vector2)transform.position).normalized;

        if (_waitTime > 0f)
        {
            _waitTime -= Time.deltaTime;
            _move.SetMoveDirection(Vector2.zero);
            _animator.SetDirection(direction);
            _coneVision.SetDirection(direction);
            return;
        }
        int random = UnityEngine.Random.Range(0, 3);
        if (Vector2.Distance(transform.position, currentPoint.Position) < _patrolPointThreshold)
        {
            _waitTime = currentPoint.WaitTime;
            _indexPatrol = (_indexPatrol + random) % _patrolPath.Length;
            currentPoint = _patrolPath.GetPoint(_indexPatrol);
        }

        SetDirection(direction);

        _move.SetPatrolSpeed();
        _coneVision.SetVisionRadius();
    }

    private void ExecuteChaseState()
    {
        if (_coneVision.Target is null)
        {
            Debug.LogWarning("Target is null in Chase state.");
            return;
        }   

        float distanceToTarget = Vector2.Distance(transform.position, _coneVision.Target.position);

        Vector2 direction = (_coneVision.Target.position - transform.position).normalized;

        if (distanceToTarget > _attackDistance)
        {
            _move.SetMoveDirection(direction);
        }
        else
        {
            _move.SetMoveDirection(Vector2.zero);
        }

        SetDirection(direction);

        _move.SetChaseSpeed();
        _coneVision.SetVisionRadius();
    }

    private void SetDirection(Vector2 direction)
    {
        _move.SetMoveDirection(direction);
        _animator.SetDirection(direction);
        _coneVision.SetDirection(direction);
    }


}