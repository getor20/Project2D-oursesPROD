using UnityEngine;
using UnityEngine.Events;

public class StatEnemy : MonoBehaviour
{
    private const int MinHealth = 0;

    [SerializeField] private EnemyStatBlock _stats;
    [SerializeField] private AudioManager _audioManager;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float SpeedPatrol { get; private set; }
    public float SpeedChase { get; private set; }
    public float Armor { get; private set; }

    public UnityEvent OnDie;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        MaxHealth = _stats.MaxHealth;
        CurrentHealth = MaxHealth;
        SpeedChase = _stats.SpeedChase;
        SpeedPatrol = _stats.SpeedPatrol;
    }

    public void TakeDamage(float damage)
    {
        float damageTake = Mathf.Max(0, /*_takeDamage.Damage*/ damage);

        CurrentHealth = Mathf.Clamp(CurrentHealth - damageTake, MinHealth, MaxHealth);

        Debug.Log($"{gameObject.name} - TakeDamage: {damageTake}, CurrentHealth: {CurrentHealth}/{MaxHealth}");

        if (CurrentHealth <= MinHealth)
        {
            Die();
        }
        else // <-- ИСПРАВЛЕНИЕ: Звук попадания, только если персонаж НЕ умер
        {
            _audioManager.PlaySFX(_audioManager.HitClip);
        }
    }

    private void Die()
    {
        OnDie?.Invoke();
        // Использование нового метода для случайного клипа смерти
        _audioManager.PlaySFX(_audioManager.CurrentDeathClip);
        // gameObject.SetActive(false);
    }
}