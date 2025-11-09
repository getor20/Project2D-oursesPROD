using UnityEngine;
using System.Collections.Generic;

public class ConeVision : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private LayerMask _obstacleLayerMask;

    [SerializeField] private float _mainVisionRadius;
    [SerializeField] private float _patrolVisionRadius = 3f;
    [SerializeField] private float _chaseVisionRadius = 6f;
    [SerializeField, Range(0, 360)] private float _visionAngle = 90f;

    private Vector2 _forwardDirection;

    private List<Transform> _targets = new List<Transform>();

    public LayerMask TargetLayerMask => _targetLayerMask;

    public void SetDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
            _forwardDirection = direction.normalized;
    }

    public void FindTarget(List<Transform> targets)
    {
        targets.Clear();
        _targets.Clear();

        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, _mainVisionRadius, _targetLayerMask);

        foreach (var targetCollider in targetsInViewRadius)
        {
            Transform target = targetCollider.transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

             if (Vector2.Angle(_forwardDirection, directionToTarget) < _visionAngle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, _obstacleLayerMask))
                {
                    targets.Add(target);
                    _targets.Add(target);
                }
            }
        }
    }

    public void SetPatrol()
    {
        _mainVisionRadius = _patrolVisionRadius;
    }

    public void SetChase()
    {
        _mainVisionRadius = _chaseVisionRadius;
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, _mainVisionRadius);

        Vector3 viewAngleA = DirectionFromAngle(-_visionAngle / 2, _forwardDirection);
        Vector3 viewAngleB = DirectionFromAngle(_visionAngle / 2, _forwardDirection);

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * _mainVisionRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * _mainVisionRadius);

        Gizmos.color = Color.red;
        foreach (var target in _targets)
        {
            if (target != null)
                Gizmos.DrawLine(transform.position, target.position);
        } 
    }

    private Vector3 DirectionFromAngle(float angleInDegrees, Vector2 forwardDirection)
    {
        float baseAngle = Mathf.Atan2(forwardDirection.y, forwardDirection.x) * Mathf.Rad2Deg;
        float totalAngle = baseAngle + angleInDegrees;

        return new Vector3(Mathf.Cos(totalAngle * Mathf.Deg2Rad), Mathf.Sin(totalAngle * Mathf.Deg2Rad));
    }
#endif
}
