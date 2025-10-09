using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LiftingObjects : MonoBehaviour
{
    private List<Food> _items = new List<Food>();        // Список всех объектов Food в мире (которые могут быть подняты)
    private Inventory _inventory;                       // Ссылка на компонент инвентаря

    public bool IsTrigger { get; private set; }         // Флаг: True, если хотя бы один предмет находится в зоне подбора

    private void Awake()
    {
        // Попытка получить компонент Inventory на том же игровом объекте
        _inventory = GetComponent<Inventory>();
        if (_inventory == null)
        {
            Debug.LogError("КРИТИЧНАЯ ОШИБКА: Компонент Inventory не найден! Подбор невозможен.", this);
        }

        // Находим все объекты типа Food в сцене при старте
        // FindObjectsByType - более современный аналог FindObjectsOfType
        _items = FindObjectsByType<Food>(FindObjectsSortMode.None).ToList();
    }

    private void Update()
    {
        // Проверяем с помощью LINQ Any(), активен ли IsTrigger хотя бы у одного предмета в списке
        IsTrigger = _items.Any(food => food != null && food.IsTrigger);
    }

    // Метод, вызываемый при взаимодействии игрока (например, нажатии кнопки "Взять")
    public void Interaction(bool isInteraction)
    {
        // Выходим, если взаимодействие неактивно или инвентарь не найден
        if (!isInteraction || _inventory == null) return;

        // Определяем критерий (условие) для подбора:
        // Объект существует, его триггер активен, и к нему прикреплены данные (ItemsStatBlock)
        System.Predicate<Food> canBePickedUp = food => food != null && food.IsTrigger && food.Data != null;

        // ШАГ 1: Сбор данных для инвентаря (используем LINQ)
        List<ItemsStatBlock> itemsDataToAdd = _items
            .Where(canBePickedUp.Invoke) // 1. Фильтруем список, оставляя только те, что можно поднять
            .Select(food => food.Data)   // 2. Извлекаем (проецируем) только объект данных (ItemsStatBlock)
            .ToList();                   // 3. Преобразуем результат в List

        // ШАГ 2: Добавление в инвентарь и очистка мира
        if (itemsDataToAdd.Count > 0)
        {
            // Передаем собранные данные в инвентарь для стекирования
            _inventory.AddItem(itemsDataToAdd);

            Debug.Log($"Собрано {itemsDataToAdd.Count} предметов для добавления.");

            // Очистка сцены:

            // 3a. Уничтожаем GameObject'ы в мире
            // Получаем список ссылок на объекты, которые нужно уничтожить
            List<Food> itemsToDestroy = _items.Where(canBePickedUp.Invoke).ToList();

            foreach (var foodItem in itemsToDestroy)
            {
                Destroy(foodItem.gameObject); // Уничтожаем визуальный объект
            }

            // 3b. Очистка внутреннего списка _items
            // Удаляем все подбираемые элементы из списка отслеживания с помощью RemoveAll
            _items.RemoveAll(canBePickedUp.Invoke);
        }
        else
        {
            Debug.LogWarning("Собрано 0 предметов для добавления (Триггер не активен или отсутствуют данные).");
        }
    }
}