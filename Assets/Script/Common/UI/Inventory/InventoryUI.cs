// InventoryUI.cs

using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Ссылка на компонент инвентаря игрока
    [SerializeField] private Inventory playerInventory;


    [SerializeField] private Transform _slotParent;

    // Массив для хранения всех компонентов InventorySlot для удобного доступа
    private InventorySlot[] _slots;


    private void Awake()
    {
        _slots = _slotParent.GetComponentsInChildren<InventorySlot>();
    }

    private void Start()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Ошибка: Ссылка на Inventory не установлена в UIController.");
            return;
        }

        // !!! ПОДПИСКА !!!
        // Подписываем метод RefreshDisplay на событие.
        playerInventory.OnInventoryUpdated += RefreshDisplay;

        // Первоначальное обновление при старте игры
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        Debug.Log("[InventoryUI] Получено событие. Обновляю отображение!");

        // 1. Получаем актуальные данные из инвентаря через публичное свойство
        var currentItems = playerInventory.Items;

        // 2. Логика очистки и обновления UI-слотов
        // ClearSlots(); 

        foreach (var itemEntry in currentItems)
        {
            int itemID = itemEntry.Key;
            int stackCount = itemEntry.Value;

            // Здесь должна быть логика поиска иконки/имени по itemID
            // И создание/обновление UI-элементов

            Debug.Log($"[UI] Рисую: ID {itemID}, Количество {stackCount}");
            // UpdateSlot(itemID, stackCount);
        }
    }

    private void OnDestroy()
    {
        // ВАЖНО: Всегда отписывайтесь от событий при уничтожении объекта UI
        if (playerInventory != null)
        {
            playerInventory.OnInventoryUpdated -= RefreshDisplay;
        }
    }
}