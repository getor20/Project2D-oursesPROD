using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; // Добавляем using System.Linq для GetValueOrDefault, если он не был добавлен

public class Inventory : MonoBehaviour
{
    // --------------------------------------------------------------------------------
    // 1. ВЛОЖЕННЫЙ СТАТИЧЕСКИЙ КЛАСС (РЕЕСТР ДАННЫХ)
    // --------------------------------------------------------------------------------
    public static class ItemDataRegistry
    {
        // !!! ИСПРАВЛЕНИЕ 1: Словарь для хранения ВСЕГО блока данных ItemsStatBlock
        public static Dictionary<int, ItemsStatBlock> RegisteredItems = new Dictionary<int, ItemsStatBlock>();

        // Оставляем словарь для спрайтов (RegisteredIcons) для GetIcon
        public static Dictionary<int, Sprite> RegisteredIcons = new Dictionary<int, Sprite>();

        /// <summary>
        /// Регистрирует полный блок данных предмета.
        /// </summary>
        public static void RegisterItemData(ItemsStatBlock data)
        {
            if (data == null || data.ID <= 0) return;

            if (!RegisteredItems.ContainsKey(data.ID))
            {
                RegisteredItems.Add(data.ID, data);
                // Регистрируем иконку для GetIcon
                RegisteredIcons.TryAdd(data.ID, data.Icon);
                Debug.Log($"Данные для предмета ID {data.ID} успешно зарегистрированы.");
            }
        }

        /// <summary>
        /// !!! ИСПРАВЛЕНИЕ 2: Метод для получения полного блока данных предмета.
        /// </summary>
        public static ItemsStatBlock GetItemData(int itemID)
        {
            RegisteredItems.TryGetValue(itemID, out ItemsStatBlock data);
            return data;
        }

        // Оригинальный метод (используется для обратной совместимости с GetIcon)
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

            // !!! ИСПРАВЛЕНИЕ 3: Регистрируем ПОЛНЫЙ блок данных.
            ItemDataRegistry.RegisterItemData(itemData);

            // 1. Получаем текущее количество.
            _items.TryGetValue(itemKey, out int currentCount);

            // 2. Устанавливаем новое значение: старое количество + 1.
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
}