using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LiftingObjects : MonoBehaviour
{
    [field: SerializeField] private List<Food> _items = new List<Food>();
    private Inventory _inventory;

    // Ссылка на InventoryUI удалена, зависимость только от Inventory
    public bool IsTrigger { get; private set; }

    private void Awake()
    {
        // Попытка получить компонент Inventory на том же игровом объекте
        _inventory = GetComponent<Inventory>();
        if (_inventory == null)
        {
            Debug.LogError("КРИТИЧНАЯ ОШИБКА: Компонент Inventory не найден! Подбор невозможен.", this);
        }

        // Находим все объекты типа Food в сцене при старте
        
    }

    private void Update()
    {
        _items = FindObjectsByType<Food>(FindObjectsSortMode.None).ToList();
        // Проверяем с помощью LINQ Any(), активен ли IsTrigger хотя бы у одного предмета в списке
        IsTrigger = _items.Any(food => food != null && food.IsTrigger);
    }

    // Метод, вызываемый при взаимодействии игрока (например, нажатии кнопки "Взять")
    public void Interaction(bool isInteraction)
    {
        // Выходим, если взаимодействие неактивно или инвентарь не найден
        if (!isInteraction || _inventory == null) return;

        // Определяем критерий (условие) для подбора
        System.Predicate<Food> canBePickedUp = food => food != null && food.IsTrigger && food.Data != null;

        // ШАГ 1: Сбор данных для инвентаря
        List<ItemsStatBlock> itemsDataToAdd = _items
            .Where(canBePickedUp.Invoke) // 1. Фильтруем список
            .Select(food => food.Data)  // 2. Извлекаем объект данных
            .ToList();

        if (itemsDataToAdd.Count > 0)
        {
            // ШАГ 1.5: ПРОВЕРКА ВМЕСТИМОСТИ ИНВЕНТАРЯ
            // Inventory сам знает свои лимиты
            if (!_inventory.CanAddItem(itemsDataToAdd))
            {
                Debug.LogWarning($"Инвентарь полон. Невозможно подобрать {itemsDataToAdd.Count} предметов.");
                return; // Прекращаем подбор
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