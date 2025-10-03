using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LiftingObjects : MonoBehaviour
{
    [SerializeField] private List<Food> _items = new List<Food>();

    private Inventory _inventory;
    private Item _item;
    public int _selectedItemIndex { get; private set; } = 100;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        _item = GetComponent<Item>();

        Food[] foundFoods = FindObjectsOfType<Food>();
        _items = foundFoods.ToList();

        Debug.Log($"Найдено объектов Food: {_items.Count}");
    }

    public void Interaction(bool isInteraction)
    { 
        for (int i = _items.Count - 1; i >= 0; i--)
        {
            Food foodItem = _items[i];

            if (foodItem != null && foodItem.IsTrigger == true && isInteraction)
            {
                if (foodItem.ID == 1)
                {
                    // Логика для ID 1
                }
                else if (foodItem.ID == 2)
                {
                    // Логика для ID 2
                }
                // Преобразуем список Food в список Item
                _item.Initialization(foodItem.ID, foodItem.Name, foodItem.Icon, foodItem.Description);
                Destroy(foodItem.gameObject);
            }
        }
    }
}