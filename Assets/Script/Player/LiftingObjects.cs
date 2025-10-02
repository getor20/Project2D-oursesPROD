using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LiftingObjects : MonoBehaviour
{
    [SerializeField] private List<Food> _items = new List<Food>();

    private Inventory _inventory;
    public int _selectedItemIndex { get; private set; } = 100;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        Food[] foundFoods = FindObjectsOfType<Food>();
        _items = foundFoods.ToList();

        Debug.Log($"Найдено объектов Food: {_items.Count}");
    }

    public void Interaction(bool isInteraction)
    { 
        // ИСПРАВЛЕНО: Используем ОБРАТНЫЙ цикл 'for' для безопасного удаления элементов (Destroy)
        for (int i = _items.Count - 1; i >= 0; i--)
        {
            Food foodItem = _items[i];

            // Проверяем, что объект еще не null (на всякий случай)
            if (foodItem != null && foodItem.IsTrigger == true && isInteraction)
            {
                if (foodItem.ID == 1)
                {
                    //Debug.Log($"Взаимодействие с Food ID {foodItem.ID}");
                    //_inventory.In();
                    //foodItem
                    // Здесь можно добавить логику для ID 1
                }
                else if (foodItem.ID == 2)
                {
                    //Debug.Log($"Взаимодействие с Food ID {foodItem.ID}");
                    // Здесь можно добавить логику для ID 2
                }
                //Debug.Log($"Взаимодействие с Food ID {foodItem.ID}");
                _inventory.In(foodItem.ID, foodItem.Name, foodItem.Icon, foodItem.Description);
                // ВАЖНО: Уничтожаем объект
                Destroy(foodItem.gameObject);

                // Объект Food должен сам вызвать DeregisterFood() в своем OnDestroy().
            }
        }
    }
}