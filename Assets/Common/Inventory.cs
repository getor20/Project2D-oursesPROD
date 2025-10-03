using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    // Список для хранения всех отдельных предметов в инвентаре
    private Dictionary<int, Item> items = new Dictionary<int, Item>();

    private void Update()
    {
        Debug.Log($"Найдено объектов Food: {items.Count}");
    }

    public void Initialization(bool isInteraction)
    {
        // ИСПРАВЛЕНО: Используем ОБРАТНЫЙ цикл 'for' для безопасного удаления элементов (Destroy)
        for (int i = items.Count - 1; i >= 0; i--)
        {
            Item item = items[i];

            
        }
    }
}