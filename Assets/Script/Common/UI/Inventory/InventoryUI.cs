using UnityEngine;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _slotParent;
    [SerializeField] private ItemDropper _itemDropper;

    [Tooltip("Максимальное количество предметов в одном UI-стеке.")]
    [field: SerializeField] public int MaxStack { get; private set; }

    [SerializeField] private Description _description;

    private InventorySlot[] _slots;

    private int _activeItemID;
    public int TotalSlotsCount => _slots != null ? _slots.Length : 0;


    private void Awake()
    {
        _slots = _slotParent.GetComponentsInChildren<InventorySlot>();

        if (_slots.Length == 0)
        {
            Debug.LogError("Не найден ни один компонент InventorySlot.");
        }
        
        // Подписываемся на событие клика каждого слота
        foreach (var slot in _slots)
        {
            // Здесь мы подписываемся на публичное событие слота
            slot.OnSlotClicked += ShowItemDescription;
        }
    }

    private void Start()
    {
        if (_inventory == null)
        {
            Debug.LogError("Ошибка: Ссылка на Inventory не установлена в InventoryUI.");
            return;
        }

        _inventory.SetInventoryLimits(TotalSlotsCount, MaxStack);
        _inventory.OnInventoryUpdated += RefreshDisplay;
    }

    public void HideDescription()
    {
        if (_description != null)
        {
            _description.Hide();
        }
        _activeItemID = 0;
    }

    // --------------------------------------------------------------------------------
    // МЕТОДЫ УПРАВЛЕНИЯ ОПИСАНИЕМ (ПОСРЕДНИК)
    // --------------------------------------------------------------------------------

    /// <summary>
    /// Вызывается InventorySlot через событие. Получает itemID и отображает описание.
    /// </summary>
    public void ShowItemDescription(int itemID)
    {
        if (_description == null)
        {
            Debug.LogError("КРИТИЧЕСКАЯ ОШИБКА: Компонент Description не привязан к InventoryUI. Проверьте Inspector.");
            return;
        }
        
        if (itemID == 0)
        {
            HideDescription();
            return;
        }

        if (_activeItemID == itemID)
        {
            HideDescription();
            return;
        }
        
        _activeItemID = itemID; 

        Debug.Log($"[UI Diagnostic] Клик по слоту! Запрошен ID: {_activeItemID}");

        ItemsStatBlock data = Inventory.ItemDataRegistry.GetItemData(_activeItemID);

        if (data != null)
        {
            Debug.Log($"[UI Diagnostic] Данные получены. Имя предмета: {data.Name}");
            _description.Show(data.Icon, data.Name, data.Description);
        }
        else
        {
            Debug.LogWarning($"[UI Diagnostic] Не найдены данные для предмета с ID: {_activeItemID}");
            HideDescription();
        }
    }

    // --------------------------------------------------------------------------------
    // МЕТОДЫ ОБНОВЛЕНИЯ ОТОБРАЖЕНИЯ
    // --------------------------------------------------------------------------------
    
    // ... (Методы RefreshDisplay, GetItemIcon, ClearAllSlots остаются без изменений) ...

    private void ClearAllSlots()
    {
        foreach (var slot in _slots)
        {
            slot.ClearSlot();
        }
    }

    private Sprite GetItemIcon(int itemID)
    {
        return Inventory.ItemDataRegistry.GetIcon(itemID);
    }

    private void RefreshDisplay()
    {
        ClearAllSlots();

        var currentItems = _inventory.Items;
        int slotIndex = 0;

        foreach (var itemEntry in currentItems)
        {
            int itemID = itemEntry.Key;
            int totalCount = itemEntry.Value;
            int remainingCount = totalCount;

            while (remainingCount > 0)
            {
                if (slotIndex >= _slots.Length)
                {
                    Debug.LogWarning($"Все UI-слоты ({_slots.Length}) заполнены.");
                    return;
                }

                InventorySlot currentSlot = _slots[slotIndex];

                int countForThisSlot = Mathf.Min(remainingCount, MaxStack);
                remainingCount -= countForThisSlot;

                Sprite itemIcon = GetItemIcon(itemID);

                currentSlot.SetItem(itemIcon, countForThisSlot, itemID);

                slotIndex++;
            }
        }
    }

    private void OnDestroy()
    {
        if (_inventory != null)
        {
            _inventory.OnInventoryUpdated -= RefreshDisplay;
        }
        
        if (_slots != null)
        {
            foreach (var slot in _slots)
            {
                if (slot != null)
                {
                    // Отписка от событий
                    slot.OnSlotClicked -= ShowItemDescription;
                }
            }
        }
    }

    public void OnDrop()
    {
        if (_activeItemID <= 0)
        {
            Debug.Log("Нет активного предмета для сброса.");
            return;
        }

        int countToDrop = 1;
        int itemIDToDrop = _activeItemID;

        int itemsRemoved = _inventory.RemoveItem(itemIDToDrop, countToDrop);

        if (itemsRemoved > 0)
        {
            _itemDropper.Drop(itemIDToDrop, itemsRemoved);

            if (!_inventory.ContainsItem(itemIDToDrop))
            {
                HideDescription();
                _activeItemID = 0;
            }
        }
    }
    public float OnUses { get; private set; }
    public void OnUse()
    {
        if (_activeItemID <= 0)
        {
            Debug.Log("Нет активного предмета для использования.");
            return;
        }

        int countToUse = 1;
        int itemIDToUse = _activeItemID;
        float edibilityValue = 0f; // Инициализация

        // 1. Получаем данные о предмете
        ItemsStatBlock itemData = Inventory.ItemDataRegistry.GetItemData(itemIDToUse);
        if (itemData == null)
        {
            Debug.LogError($"Невозможно использовать предмет ID {itemIDToUse}: данные не найдены.");
            return;
        }

        // 2. Получаем значение Edibility
        // Предполагается, что в ItemsStatBlock есть поле public float Edibility
        edibilityValue = itemData.Edibility; // <-- ПОЛУЧЕНИЕ ЗНАЧЕНИЯ

        int itemsRemoved = _inventory.RemoveItem(itemIDToUse, countToUse);

        if (itemsRemoved > 0)
        {
            // 3. Передаем значение в метод OnUse() класса Inventory
            OnUses = edibilityValue; // <-- ИЗМЕНЕНИЕ: передача значения

            if (!_inventory.ContainsItem(itemIDToUse))
            {
                HideDescription();
                _activeItemID = 0;
            }
        }
    }
}