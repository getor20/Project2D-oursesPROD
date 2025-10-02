using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public int Stack { get; private set; } // Stack может меняться, поэтому Stack++ или Stack = value
    public string Description { get; private set; }

    // Конструктор для создания нового экземпляра предмета
    public Item(int iD, string name, Sprite icon, string description, int stack = 1)
    {
        ID = iD;
        Name = name;
        Icon = icon;
        Description = description;
        Stack = stack;
    }

    // Ваш метод Initialization (можно сохранить его как альтернативу/дополнение)
    public void Initialization(int iD, string name, Sprite icon, string description)
    {
        ID = iD;
        Name = name;
        Icon = icon;
        Description = description;
        // Stack может быть установлен в 1 или оставлен как есть
        Debug.Log("Инициализация предмета ID: " + ID);
    }

    // Метод для изменения количества (стэка)
    public void SetStack(int newStack)
    {
        Stack = newStack;
        Debug.Log("Количество предметов (Stack) обновлено: " + Stack);
    }
}
