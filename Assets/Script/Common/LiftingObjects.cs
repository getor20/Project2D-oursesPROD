// LiftingObjects.cs

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LiftingObjects : MonoBehaviour
{
    private List<Food> _items = new List<Food>();
    private Inventory _inventory;

    // !!! НОВОЕ ПОЛЕ: Ссылка на InventoryUI для доступа к лимитам слотов
    [SerializeField] private InventoryUI _inventoryUI; 

    public bool IsTrigger { get; private set; }

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        if (_inventory == null)
        {
            Debug.LogError("КРИТИЧНАЯ ОШИБКА: Компонент Inventory не найден! Подбор невозможен.", this);
        }
        
        // !!! ПРОВЕРКА НАЛИЧИЯ InventoryUI
        if (_inventoryUI == null)
        {
            Debug.LogError("КРИТИЧНАЯ ОШИБКА: Ссылка на InventoryUI не установлена! Проверка заполненности невозможна.", this);
        }

        _items = FindObjectsByType<Food>(FindObjectsSortMode.None).ToList();
    }

    private void Update()
    {
        // Проверяем с помощью LINQ Any(), активен ли IsTrigger хотя бы у одного предмета в списке
        IsTrigger = _items.Any(food => food != null && food.IsTrigger);
    }

    public void Interaction(bool isInteraction)
    {
        // Выходим, если взаимодействие неактивно или инвентарь не найден
        if (!isInteraction || _inventory == null || _inventoryUI == null) return; // Добавляем проверку _inventoryUI

        // ... (определение canBePickedUp)
        System.Predicate<Food> canBePickedUp = food => food != null && food.IsTrigger && food.Data != null;

        // ШАГ 1: Сбор данных для инвентаря
        List<ItemsStatBlock> itemsDataToAdd = _items
            .Where(canBePickedUp.Invoke)
            .Select(food => food.Data)
            .ToList();

        if (itemsDataToAdd.Count > 0)
        {
            // !!! ШАГ 1.5: ПРОВЕРКА ВМЕСТИМОСТИ ИНВЕНТАРЯ !!!
            if (!_inventory.CanAddItem(itemsDataToAdd, _inventoryUI.TotalSlotsCount, _inventoryUI.MaxStack))
            {
                // Если инвентарь переполнится, выводим предупреждение и ПРЕКРАЩАЕМ подбор.
                Debug.LogWarning($"Инвентарь полон. Невозможно подобрать {itemsDataToAdd.Count} предметов. (Слотов: {_inventoryUI.TotalSlotsCount}, Макс. Стек: {_inventoryUI.MaxStack})");
                return;
            }

            // ШАГ 2: Добавление в инвентарь и очистка мира (выполняется только если CanAddItem = true)
            
            // Передаем собранные данные в инвентарь для стекирования
            _inventory.AddItem(itemsDataToAdd);

            Debug.Log($"Собрано {itemsDataToAdd.Count} предметов для добавления.");

            // ... (очистка сцены, уничтожение объектов и удаление из _items — без изменений)
            List<Food> itemsToDestroy = _items.Where(canBePickedUp.Invoke).ToList();

            foreach (var foodItem in itemsToDestroy)
            {
                Destroy(foodItem.gameObject);
            }

            _items.RemoveAll(canBePickedUp.Invoke);
        }
        else
        {
            Debug.LogWarning("Собрано 0 предметов для добавления (Триггер не активен или отсутствуют данные).");
        }
    }
}