using UnityEngine;

[CreateAssetMenu(fileName = nameof(EnemyStatBlock), menuName = "Stat/Enemies")]
public class EnemyStatBlock : ScriptableObject
{
    public float MaxHealth;
    public float SpeedPatrol;
    public float SpeedChase;
}

