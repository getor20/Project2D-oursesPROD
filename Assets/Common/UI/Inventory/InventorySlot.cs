using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private Items _item;

    // Метод для отображения предмета в слоте

    // Метод для очистки слота
    public void ClearSlot()
    {
        _item = null;
        // Логика очистки слота
    }
    public void DisplayItem(Items item)
    {
        _item = item;
        // Логика отображения предмета в слоте
    }
}
