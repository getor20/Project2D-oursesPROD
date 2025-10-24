using UnityEngine;

public class MeleeAttacker : MonoBehaviour
{
    [SerializeField] private LayerMask _target;
    [SerializeField] private BoxCollider2D _hitboxTemplate;
    [SerializeField] private float _attackCooldown;

    [SerializeField] private Animator _animator;
    private float _lastAttackTime = 0f;

    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (_hitboxTemplate != null)
        {
            _hitboxTemplate.enabled = false;
        }
    }

    public void i()
    {
        Debug.Log("Attack triggered");
    }


    public void Attack()
    {
        if (Time.time - _lastAttackTime < _attackCooldown)
        {
            return;
        }

        _lastAttackTime = Time.time;
        Debug.Log("Attack triggered");
        _animator.SetTrigger("Attack");
        Vector2 boxCenter = _hitboxTemplate.transform.position;
        Vector2 boxSize = _hitboxTemplate.size;
        float boxAngle = _hitboxTemplate.transform.eulerAngles.z;

        Collider2D[] hitTargets = Physics2D.OverlapBoxAll(boxCenter, boxSize, boxAngle, _target);

        foreach (var hit in hitTargets)
        {
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
                continue;

            if (hit.TryGetComponent<StatEnemy>(out StatEnemy targetStats))
            {
                targetStats.TakeDamage(10);
            }
        }
    }
}