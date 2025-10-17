using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; // Добавляем using System.Linq для GetValueOrDefault

public class Inventory : MonoBehaviour
{
    // ... (Остальная часть класса ItemDataRegistry остается без изменений) ...
    public static class ItemDataRegistry
    {
        // ... (Код ItemDataRegistry) ...
        public static Dictionary<int, ItemsStatBlock> RegisteredItems = new Dictionary<int, ItemsStatBlock>();
        public static Dictionary<int, Sprite> RegisteredIcons = new Dictionary<int, Sprite>();

        public static void RegisterItemData(ItemsStatBlock data)
        {
            if (data == null || data.ID <= 0) return;

            if (!RegisteredItems.ContainsKey(data.ID))
            {
                RegisteredItems.Add(data.ID, data);
                RegisteredIcons.TryAdd(data.ID, data.Icon);
                Debug.Log($"Данные для предмета ID {data.ID} успешно зарегистрированы.");
            }
        }

        public static ItemsStatBlock GetItemData(int itemID)
        {
            RegisteredItems.TryGetValue(itemID, out ItemsStatBlock data);
            return data;
        }

        public static void RegisterItemIcon(int itemID, Sprite itemSprite)
        {
            if (itemSprite == null) return;
            if (!RegisteredIcons.ContainsKey(itemID))
            {
                RegisteredIcons.Add(itemID, itemSprite);
            }
        }

        public static Sprite GetIcon(int itemID)
        {
            RegisteredIcons.TryGetValue(itemID, out Sprite icon);
            return icon;
        }
    }
    // --------------------------------------------------------------------------------


    // 2. ПОЛЯ ОСНОВНОГО КЛАССА Inventory
    private Dictionary<int, int> _items = new Dictionary<int, int>();
    public IReadOnlyDictionary<int, int> Items => _items;

    public event Action OnInventoryUpdated;

    private int _maxSlots;
    private int _maxStack;

    public void SetInventoryLimits(int totalSlots, int maxStackSize)
    {
        _maxSlots = totalSlots;
        _maxStack = maxStackSize;
    }


    public bool CanAddItem(List<ItemsStatBlock> itemsToAdd)
    {
        if (itemsToAdd == null || itemsToAdd.Count == 0) return true;

        if (_maxSlots <= 0 || _maxStack <= 0)
        {
            Debug.LogWarning("Лимиты инвентаря не установлены (InventoryUI.Start не был вызван). Подбор невозможен.");
            return false;
        }

        // 1. Симулируем добавление предметов
        Dictionary<int, int> simulatedItems = new Dictionary<int, int>(_items);

        foreach (var itemData in itemsToAdd)
        {
            if (itemData == null || itemData.ID <= 0) continue;
            int itemKey = itemData.ID;

            simulatedItems.TryGetValue(itemKey, out int currentCount);
            simulatedItems[itemKey] = currentCount + 1; // Увеличение на 1
        }

        // 2. Вычисляем общее количество требуемых UI-слотов
        int requiredSlots = 0;
        foreach (var itemEntry in simulatedItems)
        {
            int totalCount = itemEntry.Value;

            // Расчет количества стеков (Целочисленное деление с округлением вверх)
            requiredSlots += (totalCount + _maxStack - 1) / _maxStack;
        }

        // 3. Проверяем, не превышает ли требуемое количество доступное
        return requiredSlots <= _maxSlots;
    }


    // Метод, который вызывается из LiftingObjects
    public void AddItem(List<ItemsStatBlock> items)
    {
        if (items == null || items.Count == 0) return;

        int itemsAdded = 0;
        int firstAddedItemID = 0;

        foreach (var itemData in items)
        {
            if (itemData == null || itemData.ID <= 0) continue;

            int itemKey = itemData.ID;

            ItemDataRegistry.RegisterItemData(itemData);

            _items.TryGetValue(itemKey, out int currentCount);
            _items[itemKey] = currentCount + 1;

            if (itemsAdded == 0)
            {
                firstAddedItemID = itemData.ID;
            }
            itemsAdded++;
        }

        if (itemsAdded > 0)
        {
            OnInventoryUpdated?.Invoke();

            Debug.Log($"Добавлено {itemsAdded} предметов в инвентарь. Теперь предметов типа ID {firstAddedItemID} в стеке: {GetItemCount(firstAddedItemID)}.");
        }
    }

    public int GetItemCount(int itemID)
    {
        return _items.GetValueOrDefault(itemID);
    }

    // ====================================================================
    /// <summary>
    /// Проверяет, есть ли предмет с указанным ID в инвентаре (количество > 0).
    /// </summary>
    /// <param name="itemID">ID предмета.</param>
    /// <returns>True, если предмет есть в инвентаре; иначе False.</returns>
    public bool ContainsItem(int itemID)
    {
        // Проверяем, существует ли ключ И его значение больше 0.
        // GetValueOrDefault(itemID) вернет 0, если ключа нет.
        return _items.GetValueOrDefault(itemID) > 0;
    }
    // ====================================================================

    /// <summary>
    /// Удаляет указанное количество предметов из инвентаря.
    /// </summary>
    /// <param name="itemID">ID предмета для удаления.</param>
    /// <param name="countToRemove">Количество для удаления (обычно 1 или MaxStack).</param>
    /// <returns>Фактическое количество удаленных предметов.</returns>
    public int RemoveItem(int itemID, int countToRemove)
    {
        if (!_items.TryGetValue(itemID, out int currentCount))
        {
            return 0; // Предмет не найден
        }

        // Определяем фактическое количество, которое можно удалить (не больше, чем есть)
        int actualRemoved = Mathf.Min(currentCount, countToRemove);

        if (currentCount - actualRemoved <= 0)
        {
            _items.Remove(itemID); // Удаляем запись, если предметов больше не осталось
        }
        else
        {
            _items[itemID] = currentCount - actualRemoved; // Уменьшаем счетчик
        }

        if (actualRemoved > 0)
        {
            // Оповещаем UI об изменении
            OnInventoryUpdated?.Invoke();
            Debug.Log($"Удалено {actualRemoved} предметов ID {itemID}. Остаток: {GetItemCount(itemID)}.");
        }

        return actualRemoved;
    }
}