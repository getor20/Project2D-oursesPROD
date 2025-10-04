// Inventory.cs (MonoBehaviour)
using System.Collections.Generic;
using UnityEngine;
using System; // 👈 Не забудьте добавить System

public class Inventory : MonoBehaviour
{
    // InventoryItem можно оставить вне класса Inventory,
    // но в том же файле, или лучше, как вложенный класс (см. предыдущий ответ).

    // ⚠️ ВАЖНО: Класс должен быть помечен [System.Serializable]
    [Serializable]
    public class InventoryItem
    {
        // 1. Ссылка на статические данные
        public ItemsStatBlock Data;

        // 2. ⚡️ УНИКАЛЬНЫЕ ПЕРЕМЕННЫЕ, которые вы хотите доставать/менять
        public int CurrentDurability;
        public int StackSize;

        public InventoryItem(ItemsStatBlock data, int stackSize = 1)
        {
            Data = data;
            StackSize = stackSize;
            CurrentDurability = data.MaxDurability;
        }
    }

    [SerializeField] private List<InventoryItem> _items = new List<InventoryItem>();

    // Метод получения: создает контейнер состояния
    public void ReceiveAndAddItems(List<ItemsStatBlock> newItemsData)
    {
        foreach (var data in newItemsData)
        {
            // Создаём новый экземпляр состояния (InventoryItem)
            _items.Add(new InventoryItem(data, 1));
        }
    }

    // ⚡️ ДОСТАВАНИЕ УНИКАЛЬНЫХ ПЕРЕМЕННЫХ
    public int GetCurrentDurability(int index)
    {
        if (index < 0 || index >= _items.Count) return 0;

        // Достаём уникальное, текущее значение
        return _items[index].CurrentDurability;
    }

    // ⚡️ ДОСТАВАНИЕ СТАТИЧЕСКИХ ПЕРЕМЕННЫХ
    public Sprite GetItemIcon(int index)
    {
        if (index < 0 || index >= _items.Count) return null;

        // Достаём статическое значение через .Data
        return _items[index].Data.Icon;
    }

    // ... Другие методы для взаимодействия (UseItem)
}