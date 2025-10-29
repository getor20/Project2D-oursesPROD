using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LiftingObjects : MonoBehaviour
{
    [field: SerializeField] private List<Food> _items = new List<Food>();
    private Inventory _inventory;

    public bool IsTrigger { get; private set; }

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        if (_inventory == null)
        {
            Debug.LogError("КРИТИЧНАЯ ОШИБКА: Компонент Inventory не найден! Подбор невозможен.", this);
        }
    }

    private void Update()
    {
        // ПРИМЕЧАНИЕ: FindObjectsByType в Update очень ресурсоемок. 
        // Лучше использовать OnTriggerEnter/Exit для заполнения списка _items.
        _items = FindObjectsByType<Food>(FindObjectsSortMode.None).ToList();
        IsTrigger = _items.Any(food => food != null && food.IsTrigger);
    }

    public void Interaction(bool isInteraction)
    {
        if (!isInteraction || _inventory == null) return;

        // Определяем критерий (условие) для подбора
        System.Predicate<Food> canBePickedUp = food => food != null && food.IsTrigger && food.Data != null;
        // ШАГ 1: Сбор данных для инвентаря
        List<ItemsStatBlock> itemsDataToAdd = _items
            .Where(canBePickedUp.Invoke)
            .Select(food => food.Data)
            .ToList();

        if (itemsDataToAdd.Count > 0)
        {
            // ШАГ 1.5: ПРОВЕРКА ВМЕСТИМОСТИ ИНВЕНТАРЯ
            // ЭТОТ МЕТОД ТЕПЕРЬ ДОЛЖЕН РАБОТАТЬ КОРРЕКТНО, Т.К. INVENTORY ПОЛУЧИЛ ЛИМИТЫ В AWAKE()
            if (!_inventory.CanAddItem(itemsDataToAdd))
            {
                Debug.LogWarning($"Инвентарь полон. Невозможно подобрать {itemsDataToAdd.Count} предметов.");
                return;
            }

            // ШАГ 2: Добавление в инвентарь и очистка мира
            _inventory.AddItem(itemsDataToAdd);

            Debug.Log($"Собрано {itemsDataToAdd.Count} предметов для добавления.");

            // Очистка сцены:
            List<Food> itemsToDestroy = _items.Where(canBePickedUp.Invoke).ToList();

            foreach (var foodItem in itemsToDestroy)
            {
                Destroy(foodItem.gameObject); // Уничтожаем визуальный объект
            }

            // Очистка внутреннего списка _items
            _items.RemoveAll(canBePickedUp.Invoke);
        }
        else
        {
            Debug.LogWarning("Собрано 0 предметов для добавления (Триггер не активен или отсутствуют данные).");
        }
    }
}