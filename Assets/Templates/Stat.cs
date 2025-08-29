using UnityEngine;

[CreateAssetMenu(fileName =nameof(StatBlockPlayer), menuName = "Stat/Players")]
public class StatBlockPlayer : ScriptableObject
{
    public int CurrentHealth;
    public int MaxHealth;
    public int Damage;
    public int Armor;
    public float WalkingSpeed;
    public float RunSpeed;
}

[CreateAssetMenu(fileName =nameof(StatBlockEnemies), menuName = "Stat/Enemies")]
public class StatBlockEnemies : ScriptableObject
{
    public int CurrentHealth;
    public int MaxHealth;
    public int Damage;
    public float SpeedPatrol;
    public float SpeedChase;
}