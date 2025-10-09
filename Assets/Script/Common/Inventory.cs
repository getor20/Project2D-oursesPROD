using System.Collections.Generic;
using UnityEngine;
using System; // !!! НЕОБХОДИМО ДЛЯ ТИПА Action !!!

public class Inventory : MonoBehaviour
{
    // Словарь для хранения стеков: [ID Предмета] -> [Количество в стеке]
    private Dictionary<int, int> _items = new Dictionary<int, int>();
    public IReadOnlyDictionary<int, int> Items => _items;

    // Событие, которое оповещает UI и другие системы об изменении инвентаря
    public event Action OnInventoryUpdated;

    // Метод, который вызывается из LiftingObjects
    public void AddItem(List<ItemsStatBlock> items)
    {
        // Проверяем на пустоту
        if (items == null || items.Count == 0) return;

        int itemsAdded = 0;
        int firstAddedItemID = 0; // Для более безопасного лога

        foreach (var itemData in items)
        {
            // Проверка на null-ссылку и невалидный ID
            if (itemData == null || itemData.ID <= 0) continue;

            int itemKey = itemData.ID;

            // 1. Получаем текущее количество. TryGetValue вернет 0, если ID нет.
            _items.TryGetValue(itemKey, out int currentCount);

            // 2. Устанавливаем новое значение: старое количество + 1.
            _items[itemKey] = currentCount + 1;

            if (itemsAdded == 0)
            {
                firstAddedItemID = itemData.ID; // Сохраняем ID для лога
            }
            itemsAdded++;
        }

        // Если предметы были добавлены:
        if (itemsAdded > 0)
        {
            // !!! ВЫЗОВ СОБЫТИЯ !!!
            // Уведомляем всех подписчиков (например, InventoryUI), что инвентарь изменился.
            OnInventoryUpdated?.Invoke();

            // БЕЗОПАСНЫЙ LOG: Используем firstAddedItemID, который мы сохранили
            Debug.Log($"Добавлено {itemsAdded} предметов в инвентарь. Теперь предметов типа ID {firstAddedItemID} в стеке: {GetItemCount(firstAddedItemID)}.");
        }
    }

    // Метод для получения количества предмета по его ID
    public int GetItemCount(int itemID)
    {
        // Если ID есть в словаре, возвращаем количество, иначе возвращаем 0.
        return _items.GetValueOrDefault(itemID);
    }
}