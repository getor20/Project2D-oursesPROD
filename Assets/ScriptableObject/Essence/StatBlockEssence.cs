using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerStatBlock), menuName = "Stat/Players")]
public class PlayerStatBlock : ScriptableObject
{
    public float MaxHealth;
    public float Armor;
    public float Stamina;
    public float WalkingSpeed;
    public float RunSpeed;
}

[CreateAssetMenu(fileName = nameof(EnemyStatBlock), menuName = "Stat/Enemies")]
public class EnemyStatBlock : ScriptableObject
{
    public float CurrentHealth;
    public float MaxHealth;
    public float SpeedPatrol;
    public float SpeedChase;
}