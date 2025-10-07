using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Text _itemCountText;

    // Флаг, который определяет, находится ли в слоте предмет.
    // Это ключевое поле для вашего запроса.
    public bool IsOccupied { get; private set; } = false;

    // Метод для установки предмета (занимает слот)
    public void SetItem(Sprite icon, int count)
    {
        _itemIcon.sprite = icon;
        _itemCountText.text = count.ToString();
        _itemIcon.enabled = true;
        _itemCountText.enabled = true;
        IsOccupied = true;
    }

    // Метод для очистки слота (освобождает слот)
    public void ClearSlot()
    {
        _itemIcon.sprite = null;
        _itemCountText.text = "";
        _itemIcon.enabled = false;
        _itemCountText.enabled = false;
        IsOccupied = false;
    }
}