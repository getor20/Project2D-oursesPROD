using UnityEngine;
using UnityEngine.Events;

public class StatPlayer : MonoBehaviour
{
    [SerializeField] private StatBlockPlayer _stats;

    private const int MinHealth = 0;

    public float WalkingSpeed { get; private set; }
    public float RunSpeed { get; private set; }
    public float CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    public int Armor { get; private set; }
    public int Damage { get; private set; }

    public UnityEvent OnDie;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        WalkingSpeed = _stats.WalkingSpeed;
        RunSpeed = _stats.RunSpeed;
        Armor = _stats.Armor;
        MaxHealth = _stats.MaxHealth;
        CurrentHealth = _stats.CurrentHealth;
        Damage = _stats.Damage;
    }

    public void TakeDamage(int damage)
    {
        int damageTake = Mathf.Max(0, damage - Armor);

        CurrentHealth -= Mathf.Clamp(CurrentHealth - damageTake, MinHealth, MaxHealth);

        if (CurrentHealth <= MinHealth)
        {
            Die();
        }
    }
    private void Die()
    {
        OnDie.Invoke();
        Debug.Log($"{gameObject.name} Die");
    }
}
