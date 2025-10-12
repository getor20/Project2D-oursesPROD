using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemStack;

    public bool IsOccupied { get; private set; } = false;

    private void Awake()
    {
        if (_itemIcon == null || _itemStack == null)
        {
            Debug.LogError($"Ошибка: Ссылки на UI-компоненты не установлены в InventorySlot на объекте {gameObject.name}. ПРОВЕРЬТЕ INSPECTOR!");
        }
    }
    public void SetItem(Sprite icon, int count)
    {
        // Проверка от NullReferenceException
        if (_itemIcon == null || _itemStack == null) return;

        _itemIcon.sprite = icon;
        _itemStack.text = count.ToString();

        _itemIcon.enabled = true; // Image можно включать/выключать через .enabled

        // !!! ИСПРАВЛЕНИЕ: Используем SetActive() для TextMeshProUGUI !!!
        // Показываем счетчик только если предметов больше одного
        _itemStack.gameObject.SetActive(count > 1);

        IsOccupied = true;
    }

    public void ClearSlot()
    {
        // Проверка от NullReferenceException
        if (_itemIcon == null || _itemStack == null) return;

        _itemIcon.sprite = null;
        _itemStack.text = "";

        _itemIcon.enabled = false;

        // !!! ИСПРАВЛЕНИЕ: Используем SetActive(false) для TextMeshProUGUI !!!
        _itemStack.gameObject.SetActive(false);

        IsOccupied = false;
    }
}