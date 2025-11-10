using UnityEngine;
using UnityEngine.Events;

public class StatEnemy : MonoBehaviour
{
    [SerializeField] private EnemyStatBlock _stats;
    [SerializeField] private AudioManager _audioManager;

    private const int MinHealth = 0;

    public float CurrentHealth { get; private set; }
    public float Health { get; private set; }
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
        Health = _stats.MaxHealth;
        
        SpeedChase = _stats.SpeedChase;
        SpeedPatrol = _stats.SpeedPatrol;

        CurrentHealth = _stats.MaxHealth / _stats.MaxHealth;
    }

    public void SetHealth(float health)
    {
        Health = Mathf.Min(Health + health, _stats.MaxHealth);

        CurrentHealth = Health / _stats.MaxHealth;

        Debug.Log($"{gameObject.name} - SetHealth: {health}, CurrentHealth: {CurrentHealth}/{Health}");
    }

    public void TakeDamage(float damage)
    {
        float damageTake = Mathf.Max(0, damage);

        CurrentHealth = Mathf.Clamp(Health -= damageTake, MinHealth, _stats.MaxHealth) / _stats.MaxHealth;

        Debug.Log($"{gameObject.name} - TakeDamage: {damageTake}, CurrentHealth: {CurrentHealth}/{Health}");

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