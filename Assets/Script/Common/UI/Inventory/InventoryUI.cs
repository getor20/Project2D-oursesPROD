using UnityEngine;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _slotParent;
    [SerializeField] private ItemDropper _itemDropper;

    [Tooltip("Максимальное количество предметов в одном UI-стеке.")]
    [field: SerializeField] public int MaxStack { get; private set; }

    // Ссылка на компонент Description (целевой объект для отображения информации)
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
    }

    private void Start()
    {
        if (_inventory == null)
        {
            Debug.LogError("Ошибка: Ссылка на Inventory не установлена в InventoryUI.");
            return;
        }

        // Передаем лимиты UI в компонент Inventory
        _inventory.SetInventoryLimits(TotalSlotsCount, MaxStack);

        _inventory.OnInventoryUpdated += RefreshDisplay;

        // Скрываем описание при старте
        //HideDescription();
    }

    public void HideDescription()
    {
        if (_description != null)
        {
            // _description.Hide(); // Предполагаем, что этот метод существует
        }
        // --- СБРОС ID АКТИВНОГО ПРЕДМЕТА ---
        _activeItemID = 0;
    }

    // --------------------------------------------------------------------------------
    // МЕТОДЫ УПРАВЛЕНИЯ ОПИСАНИЕМ (ПОСРЕДНИК)
    // --------------------------------------------------------------------------------

    /// <summary>
    /// Вызывается InventorySlot. Получает данные и отображает описание.
    /// </summary>
    public void ShowItemDescription(int itemID)
    {
        // ПРОВЕРКА 1: Проверка ссылки на Description
        if (_description == null)
        {
            Debug.LogError("КРИТИЧЕСКАЯ ОШИБКА: Компонент Description не привязан к InventoryUI. Проверьте Inspector.");
            return;
        }

        _activeItemID = itemID;

        Debug.Log($"[UI Diagnostic] Клик по слоту! Запрошен ID: {itemID}"); // <-- ЛОГ 1

        // 1. Получаем полные данные из реестра Inventory
        ItemsStatBlock data = Inventory.ItemDataRegistry.GetItemData(itemID);

        if (data != null)
        {
            Debug.Log($"[UI Diagnostic] Данные получены. Имя предмета: {data.Name}"); // <-- ЛОГ 2

            // 2. Вызываем метод Show в Description (Description выполняет отображение)
            _description.Show(data.Icon, data.Name, data.Description);
        }
    }

    // --------------------------------------------------------------------------------
    // МЕТОДЫ ОБНОВЛЕНИЯ ОТОБРАЖЕНИЯ
    // --------------------------------------------------------------------------------

    private void ClearAllSlots()
    {
        // Вызываем ClearSlot на всех слотах, что также скроет описание, если оно было активно
        foreach (var slot in _slots)
        {
            slot.ClearSlot();
        }
    }

    private Sprite GetItemIcon(int itemID)
    {
        // Получаем иконку из реестра Inventory
        return Inventory.ItemDataRegistry.GetIcon(itemID);
    }

    /// <summary>
    /// Обновляет отображение инвентаря на основе данных из Inventory.
    /// </summary>
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

                // Передаем ID предмета в SetItem
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
    }

    // Фрагмент кода для InventoryUI.OnDrop()
    public void OnDrop()
    {
        if (_activeItemID <= 0)
        {
            Debug.Log("Нет активного предмета для сброса.");
            return;
        }

        const int countToDrop = 1; // Удаляем один предмет

        // 1. Удаляем предмет из модели Inventory
        int itemsRemoved = _inventory.RemoveItem(_activeItemID, countToDrop);

        if (itemsRemoved > 0)
        {
            // 2. Вызываем ItemDropper для спавна
            if (_itemDropper != null)
            {
                // ItemDropper сам получит PrefabObject через ItemDataRegistry
                _itemDropper.Drop(_activeItemID, itemsRemoved);
            }
            else
            {
                Debug.LogError("ItemDropper не привязан к InventoryUI!");
            }

            // 3. Скрываем описание и сбрасываем _activeItemID
            HideDescription();
        }
    }

    public void OnUse()
    {
        Debug.Log("OnUse");
    }
}