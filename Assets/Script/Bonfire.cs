using UnityEngine;
using UnityEngine.Events;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _circleCollider2D;
    [SerializeField] private EnemyCounter _enemyCounter;

    public bool IsTrigger { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Вход");
        if (_enemyCounter.ActiveEnemyCount <= 0)
        {
            IsTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Выход");
        IsTrigger = false;  
    }
}
