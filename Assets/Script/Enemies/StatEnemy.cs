using UnityEngine;

public class StatEnemy : MonoBehaviour
{
    [SerializeField] private Stat _baseStats;

    public int MaxHealth { get; private set; }

    public int CurrentHealth { get; private set; }

    public int Damage { get; private set; }

    public float SpeedPatrol { get; private set; }

    public float SpeedChase { get; private set; }

    public int Armor { get; private set; }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        MaxHealth = _baseStats.MaxHealth;
        CurrentHealth = MaxHealth;
        Damage = _baseStats.Damage;
        SpeedChase = _baseStats.SpeedChase;
        SpeedPatrol = _baseStats.SpeedPatrol;
        Armor = _baseStats.Armor;
    }
}
