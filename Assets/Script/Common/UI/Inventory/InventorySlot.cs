using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Для обработки клика

public class InventorySlot : MonoBehaviour/*, IPointerClickHandler*/
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemStack;
    [SerializeField] private InventoryUI _inventoryUI;
    [SerializeField] private GameObject _interaction;

    private int _itemID = 0; // ID предмета, хранящегося в слоте

    public bool IsOccupied { get; private set; } = false;

    private void Awake()
    {
        if (_itemIcon == null || _itemStack == null)
        {
            Debug.LogError($"Ошибка: Ссылки на UI-компоненты не установлены в InventorySlot на объекте {gameObject.name}. ПРОВЕРЬТЕ INSPECTOR!");
        }
            // Предполагаем, что InventoryUI находится где-то выше в иерархии
        _inventoryUI = GetComponentInParent<InventoryUI>();
        _itemIcon.preserveAspect = true;
        _interaction.SetActive(false);


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
        _itemStack.gameObject.SetActive(false);

        _itemID = 0; // Сбрасываем ID

        IsOccupied = false;
    }

    /// <summary>
    /// Обрабатывает клик левой кнопкой мыши по слоту.
    /// </summary>
    /// 
    private bool _isDescriptionActive = false;

    public void OnPointerClick()
    {
        _inventoryUI.ShowItemDescription(_itemID);
    }
}
