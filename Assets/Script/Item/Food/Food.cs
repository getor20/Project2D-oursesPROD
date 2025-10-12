using UnityEngine;

public class Food : MonoBehaviour
{
    [field: SerializeField] public ItemsStatBlock Data { get; private set; }

    public bool IsTrigger { get; private set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IsTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        IsTrigger = false;
    }
}