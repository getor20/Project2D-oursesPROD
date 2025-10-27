using UnityEngine;
using UnityEngine.Events;

public class StatPlayer : MonoBehaviour
{
    [SerializeField] private PlayerStatBlock _stats;

    [SerializeField] private ControllerStatBar _controllerStatBar;

    private const int MinHealth = 0;

    public float WalkingSpeed { get; private set; }
    public float RunSpeed { get; private set; }
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public float Stamina { get; private set; }
    public float CurrentStamina { get; private set; }
    public float Armor { get; private set; }

    public UnityEvent OnDie;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        WalkingSpeed = _stats.WalkingSpeed;
        RunSpeed = _stats.RunSpeed;
        Armor = _stats.Armor;
        MaxHealth = _stats.MaxHealth;
        Stamina = _stats.Stamina;

        CurrentHealth = _stats.MaxHealth;
        CurrentStamina = _stats.Stamina;
    }

    public void TakeMinStamina(float stamina)
    {
        CurrentStamina = Mathf.Clamp(Stamina -= stamina, 0, _stats.Stamina) / _stats.Stamina;
        Debug.Log($"Current Stamina: {CurrentStamina}");
    }

    public void RestoreStamina(float staminaToRestore)
    {
        // Выбираем меньшее из двух значений: (текущая + восстановление) или (максимум)
        Stamina = Mathf.Min(Stamina + staminaToRestore, _stats.Stamina);

        CurrentStamina = Stamina / _stats.Stamina;

        Debug.Log($"Stamina restored by: {staminaToRestore}. New Stamina: {Stamina}/{_stats.Stamina}");
    }

    public void TakeDamage(float damage)
    {
        float damageTake = Mathf.Max(0, damage);

        CurrentHealth = Mathf.Clamp(MaxHealth -= damageTake, MinHealth, _stats.MaxHealth) / _stats.MaxHealth;

        Debug.Log($"{gameObject.name} - TakeDamage: {damageTake}, CurrentHealth: {CurrentHealth}/{MaxHealth}");

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
