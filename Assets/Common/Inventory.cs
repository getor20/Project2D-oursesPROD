using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Inventory : MonoBehaviour
{
    // Список для хранения всех отдельных предметов в инвентаре
    public List<Item> items = new List<Item>(); 

    // Добавление нового экземпляра предмета
    public void AddNewItemInstance(int iD, string name, Sprite icon, string description)
    {
        // Создаем новый объект Item с количеством 1
        Item newItem = new Item(iD, name, icon, description, 1);
        items.Add(newItem);
        Debug.Log($"Предмет {name} (ID: {iD}) добавлен как новый экземпляр.");
    }
    
    // Получение всех предметов с определенным ID
    public List<Item> GetItemsByID(int iD)
    {
        return items.FindAll(item => item.ID == iD);
    }
}