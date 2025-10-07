// Food.cs

using UnityEngine;

public class Food : MonoBehaviour
{
    // Ссылка на данные ScriptableObject.
    // Вы будете прикреплять конкретный ItemsStatBlock через Inspector.
    [field: SerializeField] public ItemsStatBlock Data { get; private set; }

    // Это свойство должно устанавливаться через триггеры (OnTriggerEnter/Exit)
    public bool IsTrigger { get; private set; }

    // Unity-метод для обработки коллизий
    private void OnTriggerEnter2D(Collider2D collider)
    {
        IsTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        IsTrigger = false;
    }
}