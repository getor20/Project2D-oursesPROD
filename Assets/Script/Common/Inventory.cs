using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; // Убедитесь, что эта директива есть

public class Inventory : MonoBehaviour
{
    // --------------------------------------------------------------------------------
    // 1. ВЛОЖЕННЫЙ СТАТИЧЕСКИЙ КЛАСС (ЗАМЕНЯЕТ ItemDataProvider.cs)
    // Этот класс будет доступен статически как Inventory.ItemDataRegistry
    // --------------------------------------------------------------------------------
    public static class ItemDataRegistry
    {
        // Словарь для хранения спрайтов, зарегистрированных при поднятии
        public static Dictionary<int, Sprite> RegisteredIcons = new Dictionary<int, Sprite>();

        // Вызывается из экземпляра Inventory при добавлении предмета
        public static void RegisterItemIcon(int itemID, Sprite itemSprite)
        {
            if (itemSprite == null) return;

            if (!RegisteredIcons.ContainsKey(itemID))
            {
                RegisteredIcons.Add(itemID, itemSprite);
                Debug.Log($"Спрайт для предмета ID {itemID} успешно зарегистрирован.");
            }
        }

        // Вызывается из InventoryUI для получения спрайта
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

            // !!! ИСПОЛЬЗОВАНИЕ: Регистрируем спрайт через вложенный класс !!!
            // Мы предполагаем, что ItemsStatBlock.Icon содержит Sprite.
            ItemDataRegistry.RegisterItemIcon(itemData.ID, itemData.Icon);

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
        // Убедитесь, что у вас есть using System.Linq; для GetValueOrDefault
        return _items.GetValueOrDefault(itemID);
    }
}