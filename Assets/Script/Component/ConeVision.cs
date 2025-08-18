using UnityEngine;

public class ConeVision : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _mainVisionRadius;
    [SerializeField] private float _patrolVisionRadius = 3f;
    [SerializeField] private float _chaseVisionRadius = 6f;
    [SerializeField, Range(0, 360)] private float _visionAngle = 90f;

    private Vector2 _forwardDirection;

    public Transform Target => _target;

    public void SetDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
            _forwardDirection = direction.normalized;
    }

    public bool IsTargetInVision()
    {
        if (_target is null)
            return false;

        float distanceToTarget = Vector2.Distance(transform.position, _target.position);

        if (distanceToTarget > _mainVisionRadius)
            return false;

        Vector2 directionToTarget = (_target.position - transform.position).normalized;

        if (Vector2.Angle(_forwardDirection, directionToTarget) > _visionAngle / 2)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, _layerMask);

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

        if (IsTargetInVision())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _target.position);
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
