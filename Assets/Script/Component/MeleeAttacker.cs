using UnityEngine;

public class MeleeAttacker : MonoBehaviour
{
    [SerializeField] private LayerMask _target;
    [SerializeField] private GameObject _hitboxObject;
    [SerializeField] private BoxCollider2D _hitboxTemplate;
    [SerializeField] private Vector2 _boxSize = new Vector2();
    [SerializeField] private Vector2 _boxCenter = new Vector2();
    [SerializeField] private float _attackCooldown;
    
    private StatPlayer _stats;
    private Animator _animator;
    private float _lastAttackTime = -1f;

    private void Awake()
    {
        _stats = GetComponent<StatPlayer>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (_hitboxTemplate != null)
        {
            _hitboxTemplate.enabled = false;
        }
    }

    public void Attack()
    {
        if (Time.time - _lastAttackTime < _attackCooldown)
        {
            return;
        }

        _lastAttackTime = Time.time;
        _animator.SetTrigger("Attack");
        //_animator.Play("Attack");
        Vector2 boxCenter = _hitboxTemplate.transform.position;
        Vector2 boxSize = _hitboxTemplate.size;
        //float boxAngle = _hitboxTemplate.transform.eulerAngles.z;
        //float boxAngle_ = _hitboxObject.transform.eulerAngles.z;

        //Debug.Log($"Box Center: {boxCenter}, Box Size: {boxSize}, Box Angle: {boxAngle}");

        Collider2D[] hitTargets = Physics2D.OverlapBoxAll(_boxCenter, _boxSize, _animator.transform.eulerAngles.z, _target);

        Debug.Log($"{hitTargets.Length}");

        foreach (var hit in hitTargets)
        {
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
                continue;

            if (hit.TryGetComponent<StatEnemy>(out StatEnemy targetStats))
            {
                targetStats.TakeDamage(_stats.Damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Set the color of the gizmo for better visibility
        Gizmos.color = Color.cyan;

        // Store the current Gizmos matrix to restore it later
        Matrix4x4 originalMatrix = Gizmos.matrix;

        // Create a new matrix to handle the position, rotation, and scale of our box
        // The center of the box is _boxCenter relative to the object's transform position.
        // The rotation is taken from the Z-axis of the _hitboxObject's Euler angles.
        Gizmos.matrix = Matrix4x4.TRS(
            transform.position + new Vector3(_boxCenter.x, _boxCenter.y, 0),
            Quaternion.Euler(0, 0, _hitboxObject.transform.eulerAngles.z),
            Vector3.one
        );

        // Draw the wireframe cube. We use Vector3.zero for the position
        // because the transformation matrix already handles the offset.
        // We convert _boxSize to a Vector3 for the gizmo method.
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_boxSize.x, _boxSize.y, 0));

        // Restore the original Gizmos matrix to avoid affecting other gizmos
        Gizmos.matrix = originalMatrix;
    }
}