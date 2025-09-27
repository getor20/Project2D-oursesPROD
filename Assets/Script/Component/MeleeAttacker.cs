using UnityEngine;

public class MeleeAttacker : MonoBehaviour
{
    [SerializeField] private LayerMask _target;
    [SerializeField] private BoxCollider2D _hitboxTemplate;
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
        Vector2 boxCenter = transform.position;
        Vector2 boxSize = _hitboxTemplate.size;
        float boxAngle = transform.eulerAngles.z;

        //Debug.Log($"Box Center: {boxCenter}, Box Size: {boxSize}, Box Angle: {boxAngle}");

        Collider2D[] hitTargets = Physics2D.OverlapBoxAll(boxCenter, boxSize, boxAngle, _target);

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
}