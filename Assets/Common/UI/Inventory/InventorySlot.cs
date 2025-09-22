using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private Item _item;

    // Метод для отображения предмета в слоте

    // Метод для очистки слота
    public void ClearSlot()
    {
        _item = null;
        // Логика очистки слота
    }
    public void DisplayItem(Item item)
    {
        _item = item;
        // Логика отображения предмета в слоте
    }
}
