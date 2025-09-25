using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    private Items _item;

    private void Start()
    {
        // DisplayItem();
        //ClearSlot();
    }
    // Метод для отображения предмета в слоте
    public void DisplayItem()
    {
       // _icon.sprite = _item.Icon;
        _icon.enabled = true; // Делаем иконку видимой.
    }
    
    // Метод для очистки слота
    public void ClearSlot()
    {
        //_icon.sprite = null;
        _icon.enabled = false; // Делаем иконку невидимой.
    }
}

