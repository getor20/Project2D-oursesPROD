using UnityEngine;
using System.Collections.Generic;

public class ScrapperAI : MonoBehaviour
{
    public enum ScrapperState
    {
        Patrol,
        Chase
    }

    
    [SerializeField] private ConeVision _coneVision;
    [SerializeField] private PatrolPath _patrolPath;
    [SerializeField] private float _patrolPointThreshold = 0.5f;    
    [SerializeField] private float _attackDistance = 2;
    [SerializeField] private float _waitTime = 0f;
    [SerializeField] private float _timerDelay = 0.1f;
    [SerializeField] private int _indexPatrol = 0;

    private StatEnemy _stats;
    private ScrapperMove _move;
    private ScrapperAnimator _animator;
    private Transform _target;
    private readonly List<Transform> _targetsBuffer = new List<Transform>();

    private float _timer;

    private ScrapperState _enemyState;

    private void Awake()
    {
        _move = GetComponent<ScrapperMove>();
        _animator = GetComponent<ScrapperAnimator>();
        _stats = GetComponent<StatEnemy>();
    }

    private void Start()
    {
        transform.position = _patrolPath.GetPoint(_indexPatrol).Position;

        _enemyState = ScrapperState.Patrol;
    }

    private void Update()
    {
        RunFSM();
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        _coneVision.FindTarget(_targetsBuffer);

        if (_targetsBuffer.Count > 0)
        {
            _target = _targetsBuffer[0];
            SwitchState(ScrapperState.Chase);
        }
        else if (_targetsBuffer != null)
        {
            _target = null;
            SwitchState(ScrapperState.Patrol);
        }

    }

    private void SwitchState(ScrapperState newState)
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
            case ScrapperState.Patrol:
                ExecutePatrolState();
                break;
            case ScrapperState.Chase:
                ExecuteChaseState();
                break;
        }
    }

    private void ExecutePatrolState()
    {

        PatrolPoint currentPoint = _patrolPath.GetPoint(_indexPatrol);
        Vector2 direction = (currentPoint.Position - (Vector2)transform.position).normalized;

        if (_waitTime > 0f)
        {
            _waitTime -= Time.deltaTime;
            _move.SetMoveDirection(Vector2.zero);
            _coneVision.SetDirection(direction);
            return;
        }

        if (Vector2.Distance(transform.position, currentPoint.Position) < _patrolPointThreshold)
        {
            _waitTime = currentPoint.WaitTime;
            _indexPatrol = Random.Range(0, _patrolPath.Length);
        }

        
        _move.SetMoveDirection(direction);
        _animator.SetDirection(direction);
        _coneVision.SetDirection(direction);
        _coneVision.SetPatrol();
        _move.SetSpeed(_stats.SpeedPatrol);
    }

    private void ExecuteChaseState()
    {
        float distanceToTarget = Vector2.Distance(transform.position, _target.position);

        Vector2 direction = (_target.position - transform.position).normalized;

        float speed = _target.GetComponent<Rigidbody2D>().velocity.magnitude;

        Debug.Log(speed);

        if (distanceToTarget >= _attackDistance && speed >= 0)
        {
            _move.SetMoveDirection(direction);
        }
        else if (distanceToTarget <= _attackDistance && speed <= 0)
        {
            _move.SetMoveDirection(Vector2.zero);
        }

        _animator.SetDirection(direction);
        _coneVision.SetDirection(direction);
        _coneVision.SetChase();

        _move.SetSpeed(_stats.SpeedChase);
    }
}