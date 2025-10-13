using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _slotParent;

    [field: SerializeField] public int MaxStack { get; private set; }

    private InventorySlot[] _slots;

    // Новое свойство для получения общего количества доступных UI слотов
    public int TotalSlotsCount => _slots != null ? _slots.Length : 0;


    private void Awake()
    {
        // Получаем все компоненты InventorySlot, прикрепленные к дочерним объектам _slotParent
        _slots = _slotParent.GetComponentsInChildren<InventorySlot>();

        if (_slots.Length == 0)
        {
            Debug.LogError("Не найден ни один компонент InventorySlot. Убедитесь, что слоты правильно настроены.", this);
        }
    }

    private void Start()
    {
        if (_inventory == null)
        {
            Debug.LogError("Ошибка: Ссылка на Inventory не установлена в UIController. Перетащите компонент Inventory в Инспекторе.", this);
            return;
        }

        // Подписываемся на событие обновления инвентаря
        _inventory.OnInventoryUpdated += RefreshDisplay;

        // Первое отображение при старте
        RefreshDisplay();
    }

    /// <summary>
    /// Полностью очищает все UI-слоты, делая их пустыми.
    /// </summary>
    private void ClearAllSlots()
    {
        foreach (var slot in _slots)
        {
            // Использует метод ClearSlot из InventorySlot
            slot.ClearSlot();
        }
    }

    /// <summary>
    /// Получает спрайт предмета, обращаясь к статическому реестру, 
    /// который был заполнен классом Inventory при подборе предмета.
    /// </summary>
    private Sprite GetItemIcon(int itemID)
    {
        // Используем статический вложенный класс Inventory.ItemDataRegistry
        Sprite icon = Inventory.ItemDataRegistry.GetIcon(itemID);

        if (icon == null)
        {
            Debug.LogWarning($"Иконка для ID {itemID} не найдена. Проверьте, что предмет имеет спрайт и был зарегистрирован.");
        }

        return icon;
    }

    /// <summary>
    /// Основной метод: обновляет отображение инвентаря на основе данных из Inventory.
    /// Реализует логику разделения стеков по MAX_UI_STACK_SIZE.
    /// </summary>
    private void RefreshDisplay()
    {
        //Debug.Log("[InventoryUI] Обновление отображения.");

        // 1. Очищаем все слоты перед обновлением
        ClearAllSlots();

        // Получаем текущие данные инвентаря [ID -> TotalCount]
        var currentItems = _inventory.Items;
        int slotIndex = 0; // Индекс для перебора UI-слотов

        // 2. Проходим по всем уникальным типам предметов в инвентаре
        foreach (var itemEntry in currentItems)
        {
            int itemID = itemEntry.Key;
            int totalCount = itemEntry.Value;
            int remainingCount = totalCount;

            // 3. Логика разделения стека (Stack Splitting)
            // Продолжаем, пока не отобразим все количество предмета
            while (remainingCount > 0)
            {
                // Проверка на заполненность UI (если UI слотов меньше, чем нужно)
                if (slotIndex >= _slots.Length)
                {
                    Debug.LogWarning($"Все UI-слоты ({_slots.Length}) заполнены. Не могу отобразить оставшиеся предметы.");
                    return;
                }

                InventorySlot currentSlot = _slots[slotIndex];

                // Определяем, сколько поместится в текущий слот
                int countForThisSlot = Mathf.Min(remainingCount, MaxStack);
                remainingCount -= countForThisSlot;

                // Получаем иконку предмета
                Sprite itemIcon = GetItemIcon(itemID);

                if (itemIcon == null)
                {
                    // Если иконка не найдена, пропускаем этот предмет и переходим к следующему типу
                    break;
                }

                // Устанавливаем иконку и количество в слот
                currentSlot.SetItem(itemIcon, countForThisSlot);

                // Переходим к следующему UI-слоту
                slotIndex++;
            }
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события, чтобы избежать утечек памяти или ошибок
        if (_inventory != null)
        {
            _inventory.OnInventoryUpdated -= RefreshDisplay;
        }
    }
}