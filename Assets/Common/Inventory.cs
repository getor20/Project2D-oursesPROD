using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Словарь для хранения стеков: [ID Предмета] -> [Количество в стеке]
    private Dictionary<int, int> _items = new Dictionary<int, int>();

    // Метод, который вызывается из LiftingObjects
    public void AddItem(List<ItemsStatBlock> items)
    {
        // Проверяем на пустоту
        if (items == null || items.Count == 0) return;

        int itemsAdded = 0;

        foreach (var itemData in items)
        {
            // Пропускаем, если ID невалиден
            if (itemData.ID <= 0) continue;

            int itemKey = itemData.ID;

            // 1. Получаем текущее количество (currentCount) по ID. 
            // Если ID нет в словаре, TryGetValue вернет 0 (значение по умолчанию для int).
            _items.TryGetValue(itemKey, out int currentCount);

            // 2. Устанавливаем новое значение: старое количество + 1.
            // Этот синтаксис одновременно добавляет новый ключ И обновляет существующий.
            _items[itemKey] = currentCount + 1;

            itemsAdded++;
        }

        Debug.Log($"Добавлено {itemsAdded} предметов в инвентарь. Теперь предметов типа ID {items[0].ID} в стеке: {GetItemCount(items[0].ID)}.");
    }

    // Метод для получения количества предмета по его ID
    public int GetItemCount(int itemID)
    {
        // Если ID есть в словаре, возвращаем количество, иначе возвращаем 0.
        return _items.GetValueOrDefault(itemID);
    }
}