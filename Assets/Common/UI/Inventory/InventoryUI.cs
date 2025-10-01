using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    // Ссылка на ваш объект "Slots", который является родителем всех "Slot"
    [SerializeField] private Transform _slotParent;

    // Массив для хранения всех компонентов InventorySlot для удобного доступа
    private InventorySlot[] _slots;

    private void Awake()
    {
        InitializeSlots();
    }

    // Инициализация: получаем все компоненты InventorySlot с дочерних объектов _slotParent
    private void InitializeSlots()
    {
        // Получаем все компоненты InventorySlot с дочерних объектов (слотов)
        _slots = _slotParent.GetComponentsInChildren<InventorySlot>();

        if (_slots.Length == 0)
        {
            Debug.LogError("В контейнере слотов (_slotParent) не найдено ни одного компонента InventorySlot!");
        }
        else
        {
            Debug.Log($"Инициализировано {_slots.Length} слотов.");
        }
    }

    private void Update()
    {
        FindEmptySlot();
    }

    /// <summary>
    /// Перебирает все слоты и возвращает первый свободный (не занятый) слот.
    /// </summary>
    /// <returns>Первый свободный InventorySlot или null, если все заняты.</returns>
    public InventorySlot FindEmptySlot()
    {
        // Перебор (итерация) по массиву слотов
        foreach (InventorySlot slot in _slots)
        {
            // Проверка: является ли слот свободным?
            if (!slot.IsOccupied)
            {
                Debug.Log($"Найден свободный слот: {slot.name}");
                return slot; // Возвращаем найденный свободный слот
            }
        }

        Debug.LogWarning("Все слоты инвентаря заняты.");
        return null; // Все слоты заняты
    }

    // Пример использования (вы можете вызвать этот метод из Inventory для добавления предмета)
    public void AddItemToFirstEmptySlot(Sprite itemIcon, int stackCount)
    {
        InventorySlot emptySlot = FindEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.SetItem(itemIcon, stackCount);
        }
    }
}