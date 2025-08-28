using UnityEngine;
using UnityEngine.Events;

public class StatEnemy : MonoBehaviour
{
    private const int MinHealth = 0;

    [SerializeField] private Stat _stats;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public int Damage { get; private set; }
    public float SpeedPatrol { get; private set; }
    public float SpeedChase { get; private set; }
    public int Armor { get; private set; }

    public UnityEvent OnDie;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        MaxHealth = _stats.MaxHealth;
        CurrentHealth = MaxHealth;
        Damage = _stats.Damage;
        SpeedChase = _stats.SpeedChase;
        SpeedPatrol = _stats.SpeedPatrol;
        Armor = _stats.Armor;
    }

    public void TakeDamage(int damage)
    {
        int damageTake = Mathf.Max(0, damage - Armor);

        CurrentHealth = Mathf.Clamp(damageTake - CurrentHealth, MinHealth, MaxHealth);

        Debug.Log($"{gameObject.name} - TakeDamage: {damageTake}, CurrentHealth: {CurrentHealth}/{MaxHealth}");

        if (CurrentHealth <= MinHealth)
        {
            Die();
        }
    }
    private void Die()
    {
        OnDie?.Invoke();

        gameObject.SetActive(false);
    }
}