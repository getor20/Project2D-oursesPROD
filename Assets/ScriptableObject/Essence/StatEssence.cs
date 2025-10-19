using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerStatBlock), menuName = "Stat/Players")]
public class PlayerStatBlock : ScriptableObject
{
    public int CurrentHealth;
    public int MaxHealth;
    public int Damage;
    public int Armor;
    public float WalkingSpeed;
    public float RunSpeed;
    public float Stamina;
}

[CreateAssetMenu(fileName = nameof(EnemyStatBlock), menuName = "Stat/Enemies")]
public class EnemyStatBlock : ScriptableObject
{
    public int CurrentHealth;
    public int MaxHealth;
    public int Damage;
    public float SpeedPatrol;
    public float SpeedChase;
}