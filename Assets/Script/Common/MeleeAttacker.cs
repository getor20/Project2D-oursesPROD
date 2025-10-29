using UnityEngine;

public class MeleeAttacker : MonoBehaviour
{
    //private LayerMask _target;
    [SerializeField] private StatBlockSword _weaponStat;
    [SerializeField] private BoxCollider2D _hitboxTemplate;
    [SerializeField] private float _attackCooldown = 0.5f;

    [SerializeField] private Animator _animator;
    private float _lastAttackTime = 0f;

    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }

    /*private void OnEnable()
    {
        if (_hitboxTemplate != null)
        {
            _hitboxTemplate.enabled = false;
        }
    }*/

    public void SetTarget(LayerMask target)
    {
               // _target = target;
    }

    public void Attack(LayerMask target)
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

        Collider2D[] hitTargets = Physics2D.OverlapBoxAll(boxCenter, boxSize, boxAngle, target);

        foreach (var hit in hitTargets)
        {
            

            /*if (hit.TryGetComponent<TakeDamage>(out TakeDamage takeDamage))
            {
                takeDamage.IsDamage(10);
            }*/


            if (hit.TryGetComponent<StatEnemy>(out StatEnemy statEnemy))
            {
                statEnemy.TakeDamage(_weaponStat.damage);
            }

            if (hit.TryGetComponent<StatPlayer>(out StatPlayer statPlayer))
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                    Debug.LogError("yyy");

                statPlayer.TakeDamage(_weaponStat.damage);
            }
        }
    }
}