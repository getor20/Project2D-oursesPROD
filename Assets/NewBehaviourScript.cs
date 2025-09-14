/*using UnityEngine;
using System.Collections.Generic;

public class ConeVisioni : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private LayerMask _obstacleLayerMask;

    [SerializeField] private float _mainVisionRadius;
    [SerializeField] private float _patrolVisionRadius = 3f;
    [SerializeField] private float _chaseVisionRadius = 6f;
    [SerializeField, Range(0, 360)] private float _visionAngle = 90f;

    private Vector2 _forwardDirection;

    private List<Transform> _targets = new List<Transform>();

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
    }*/

   /* public bool IsTargetInVision()
    {
        if (_targetLayerMask is null)
            return false;

        float distanceToTarget = Vector2.Distance(transform.position, _targetLayerMask.position);

        if (distanceToTarget > _mainVisionRadius)
            return false;

        Vector2 directionToTarget = (_targetLayerMask.position - transform.position).normalized;

        if (Vector2.Angle(_forwardDirection, directionToTarget) > _visionAngle / 2)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, _obstacleLayerMask);

        if (hit.collider is not null)
            return false;

        return true;
    }

    public void SetVisionRadius()
    {
        _mainVisionRadius = IsTargetInVision() ? _chaseVisionRadius : _patrolVisionRadius;
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
}*/
