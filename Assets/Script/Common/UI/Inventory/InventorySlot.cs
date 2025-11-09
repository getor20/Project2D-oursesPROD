using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System; // Для Action
// IPointerClickHandler удален

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemStack;

    // ВНИМАНИЕ: Если используете компонент Button, убедитесь, что он привязан
    // [SerializeField] private Button _slotButton; // Можно добавить для явной ссылки

    public int _itemID { get; private set; } = 0;

    public bool IsOccupied { get; private set; } = false;

    // СОБЫТИЕ: InventoryUI будет подписываться на него.
    public event Action<int> OnSlotClicked;

    private void Awake()
    {
        if (_itemIcon == null || _itemStack == null)
        {
            Debug.LogError($"Ошибка: Ссылки на UI-компоненты не установлены в InventorySlot на объекте {gameObject.name}. ПРОВЕРЬТЕ INSPECTOR!");
        }
        _itemIcon.preserveAspect = true;
    }

    private void Start()
    {
        ClearSlot();
    }

    /// <summary>
    /// Устанавливает предмет в слот и сохраняет его ID.
    /// </summary>
    public void SetItem(Sprite icon, int count, int id)
    {
        if (_itemIcon == null || _itemStack == null) return;

        _itemID = id; // Сохраняем ID

        _itemIcon.sprite = icon;
        _itemStack.text = count.ToString();

        _itemIcon.enabled = true;
        _itemStack.gameObject.SetActive(count > 1);

        IsOccupied = true;
    }

    /// <summary>
    /// Очищает слот.
    /// </summary>
    public void ClearSlot()
    {
        if (_itemIcon == null || _itemStack == null) return;

        _itemIcon.sprite = null;
        _itemStack.text = "";

        _itemIcon.enabled = false;
        //_itemStack.gameObject.SetActive(false);

        _itemID = 0; // Сбрасываем ID

        IsOccupied = false;
    }

    /// <summary>
    /// Этот метод должен быть привязан к событию OnClick компонента Unity Button
    /// </summary>
    public void OnPointerClick()
    {
        // Вызываем событие, передавая свой _itemID.
        OnSlotClicked?.Invoke(_itemID);
        // Debug.Log($"Slot clicked. ItemID: {_itemID}");
    }
}