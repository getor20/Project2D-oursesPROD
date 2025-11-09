using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField] private List<PatrolPoint> _patrolPoints = new List<PatrolPoint>();
    [SerializeField] private Color _pathColor = Color.green;

    [SerializeField][Range(0f, 2f)] private float _labelOffsetY = 0.5f;
    [SerializeField] private float _arrowSize = 0.5f;
    [SerializeField][Range(10f, 45f)] private float _arrowAngle = 25f;

    public int Length => _patrolPoints.Count;

    public PatrolPoint GetPoint(int index)
    {
        if (index < 0 || index >= _patrolPoints.Count)
        {
            string errorMessage = $"[{nameof(PatrolPoint)}] неверный индекс {index}. всего точек: {Length}";
            Debug.LogError(errorMessage);
            throw new System.ArgumentOutOfRangeException(errorMessage);
        }
        return _patrolPoints[index];
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _patrolPoints.Clear();
        foreach (Transform child in transform)
        {
            PatrolPoint point = child.GetComponent<PatrolPoint>();
            if (point is not null)
            {
                _patrolPoints.Add(point);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (_patrolPoints == null || _patrolPoints.Count == 0)
            return;

        Handles.color = _pathColor;
        for (int i = 0; i < _patrolPoints.Count; i++)
        {
            if (_patrolPoints[i] == null) continue;

            Vector3 pos = _patrolPoints[i].transform.position;

            Vector3 labelPos = pos - Vector3.up * _labelOffsetY;
            Handles.Label(labelPos, i.ToString());

            Vector3 nextPos = _patrolPoints[(i + 1) % _patrolPoints.Count].transform.position;
            Handles.DrawLine(pos, nextPos);

            DrawArrow(pos, nextPos);
        }
    }

    private void DrawArrow(Vector3 from, Vector3 to)
    {
        Vector3 direction = (to - from).normalized;
        Vector3 middle = Vector3.Lerp(from,to, 0.5f);

        Handles.DrawLine(middle, middle - direction * _arrowSize);

        Vector3 right = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, _arrowAngle) * Vector3.up;
        Vector3 left = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, -_arrowAngle) * Vector3.up;

        Handles.DrawLine(middle, middle - right * _arrowSize);
        Handles.DrawLine(middle, middle - left * _arrowSize);

    }
#endif
}
