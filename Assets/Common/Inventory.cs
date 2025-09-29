using Assets.Script.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{ 
    [SerializeField] private List<Food> _items = new List<Food>();

    private PlayerController _playerController;

    public int _selectedItemIndex { get; private set; } = 100;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();

        Food[] foundFoods = FindObjectsOfType<Food>();
        _items = foundFoods.ToList();

        Debug.Log($"Найдено объектов Food: {_items.Count}");
    }

    private void Update()
    {
        Interaction();
    }

    private void Interaction()
    {
        // ИСПРАВЛЕНО: Используем ОБРАТНЫЙ цикл 'for' для безопасного удаления элементов (Destroy)
        for (int i = _items.Count - 1; i >= 0; i--)
        {
            Food foodItem = _items[i];

            // Проверяем, что объект еще не null (на всякий случай)
            if (foodItem != null && foodItem.IsTrigger == true && _playerController.IsInteraction)
            {
                if (foodItem.ID == 1)
                {
                    Debug.Log($"Взаимодействие с Food ID {foodItem.ID}");
                    //foodItem
                    // Здесь можно добавить логику для ID 1
                }
                else if (foodItem.ID == 2)
                {
                    Debug.Log($"Взаимодействие с Food ID {foodItem.ID}");
                    // Здесь можно добавить логику для ID 2
                }
                else
                {
                    Debug.Log($"Взаимодействие с Food ID {foodItem.ID}");
                    // Логика для других ID
                }
                // ВАЖНО: Уничтожаем объект
                Destroy(foodItem.gameObject);

                // Объект Food должен сам вызвать DeregisterFood() в своем OnDestroy().
            }
        }
    }
}