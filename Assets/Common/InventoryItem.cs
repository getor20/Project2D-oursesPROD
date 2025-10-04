// InventoryItem.cs (Класс данных, [System.Serializable])
using System;
using UnityEngine;

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
        // Копируем базовое значение прочности в уникальное состояние
        CurrentDurability = data.MaxDurability;
    }
}