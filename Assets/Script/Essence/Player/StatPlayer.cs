using UnityEngine;
using UnityEngine.Events;

public class StatPlayer : MonoBehaviour
{
    [SerializeField] private PlayerStatBlock _stats;

    [SerializeField] private ControllerStatBar _controllerStatBar;

    private const int MinHealth = 0;

    public float WalkingSpeed { get; private set; }
    public float RunSpeed { get; private set; }
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }
    public float Stamina { get; private set; }
    public float CurrentStamina { get; private set; }
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
        Stamina = _stats.Stamina;
    }

    public void TakeMinStamina(float stamina)
    {
        CurrentStamina = Mathf.Clamp(Stamina -= stamina, 0, _stats.Stamina) / _stats.Stamina;
        Debug.Log($"Current Stamina: {CurrentStamina}");
        //_controllerStatBar.UpdateStaminaBar(CurrentStamina / _stats.Stamina);
    }

    public void RestoreStamina(float staminaToRestore)
    {
        // 1. Increase current stamina by the restoration amount.
        CurrentStamina += staminaToRestore;

        // 2. Clamp (limit) the current stamina.
        // Ensure CurrentStamina does not go below 0 (MinStamina, which is 0f) 
        // and does not exceed the maximum capacity (Stamina).
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, Stamina);

        // Optional: Log the result for debugging.
        Debug.Log($"Stamina restored: {staminaToRestore}. Current Stamina: {CurrentStamina}/{Stamina}");
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
