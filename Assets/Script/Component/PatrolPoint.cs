using Unity.VisualScripting;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField]
    private Color _color = Color.red;

    [SerializeField]
    private float _radius = 0.5f;

    [Range(0f, 2f)]
    [SerializeField]
    private float _labelOffsetY  = 0.5f;

    [SerializeField]
    private float _arrowSize = 0.4f;

    [Range(10f, 45f)]
    [SerializeField]
    private float _arrowAngle = 25f;

    public int PointCount => transform.childCount;

    public Transform GetPointTransform(int index)
    {
        Transform child = transform.GetChild(index);

        if (PointCount <= index || child is null)
        {
            return transform;
        }

        return child;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (PointCount == 0)
        {
            return;
        }

        for (int i = 0; i < PointCount; i++)
        {
            Vector3 currentPoint = GetPointTransform(i).position;

            Gizmos.color = _color;
            Gizmos.DrawWireSphere(currentPoint, _radius);

            UnityEditor.Handles.color = _color; 
            string pointLabel = i.ToString();
            Vector3 labelPosition = currentPoint + (Vector3.up * _labelOffsetY);
            UnityEditor.Handles.Label(labelPosition, pointLabel);

            if (PointCount > 1)
            {
                Vector3 nextPoint = GetPointTransform((i + 1) % PointCount).position;
                DrawArrow(currentPoint, nextPoint);
            }
        }
    }

    private void DrawArrow(Vector3 start, Vector3 end)
    {
        UnityEditor.Handles.color = _color;
        UnityEditor.Handles.DrawLine(start, end);

        Vector3 direction = (end - start).normalized;
        if (direction == Vector3.zero)
        {
            return;
        }

        Vector3 rightWing = Quaternion.Euler(0, 0, _arrowAngle) * (-direction);
        Vector3 leftWing = Quaternion.Euler(0, 0, _arrowAngle) * (-direction);

        UnityEditor.Handles.DrawLine(end, end + rightWing * _arrowSize);
        UnityEditor.Handles.DrawLine(end, end + leftWing * _arrowSize);

    }

#endif

}
