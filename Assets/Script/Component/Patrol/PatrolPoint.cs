using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField] private Color _pointColor = Color.yellow;
    [SerializeField] private float _radius = 0.5f;
    [SerializeField][Min(0)] private float _waitTime = 0f;

    public Vector2 Position => transform.position;
    public float WaitTime => _waitTime;

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = _pointColor;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

#endif
}
