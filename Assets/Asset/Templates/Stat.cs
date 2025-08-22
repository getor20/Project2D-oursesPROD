using UnityEngine;

[CreateAssetMenu]
public class Stat : ScriptableObject
{
    public int MaxHealth = 100;
    public int Damage = 10;
    public int Armor = 0;
    public float SpeedPatrol = 3f;
    public float SpeedChase = 5f;
}