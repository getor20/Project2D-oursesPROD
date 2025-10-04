using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LiftingObjects : MonoBehaviour
{
    // ⚠️ Убран [SerializeField] или заменен на [NonSerialized] для предотвращения 
    // ошибок Editor'а при уничтожении объектов, на которые он ссылается.
    [NonSerialized] private List<Food> _items = new List<Food>();

    private Inventory _inventory;
    // ... другие поля ...

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();

        // FindObjectsOfType() остается для начальной инициализации
        Food[] foundFoods = FindObjectsOfType<Food>();
        _items = foundFoods.ToList();
        Debug.Log($"Найдено предметов Food: {_items.Count}");
    }

    public void Interaction(bool isInteraction)
    {
        if (!isInteraction) return;

        List<ItemsStatBlock> itemsDataToAdd = new List<ItemsStatBlock>();

        for (int i = _items.Count - 1; i >= 0; i--)
        {
            Food foodItem = _items[i];

            if (foodItem != null && foodItem.IsTrigger == true)
            {
                // 1. Извлекаем данные (ScriptableObject)
                if (foodItem.Data != null)
                {
                    itemsDataToAdd.Add(foodItem.Data);
                }

                _items.RemoveAt(i);
                // 2. Уничтожаем GameObject в мире
                Destroy(foodItem.gameObject);
            }
        }

        // 3. Отправляем только ДАННЫЕ в инвентарь
        if (_inventory != null && itemsDataToAdd.Count > 0)
        {
            _inventory.ReceiveAndAddItems(itemsDataToAdd);
        }
    }
}