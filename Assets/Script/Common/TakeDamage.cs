using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public int Damage {  get; private set; }

    public void IsDamage(int damage)
    {
        //Debug.LogError($"{gameObject.name} took {damage} damage.");
        Damage = damage;
    }

}
