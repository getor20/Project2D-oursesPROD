using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerStatBlock), menuName = "Stat/Players")]

public class PlayerStatBlock : ScriptableObject
{
    public float MaxHealth;
    public float Armor;
    public float Stamina;
    public float WalkingSpeed;
    public float RunSpeed;
    public float SlowSpeed;
}

