using UnityEngine;

public class Inventory : MonoBehaviour
{
    // масив хранения
    //private 

    public int ID { get; private set; }
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public int Stack { get; private set; }
    public string Description { get; private set; }

    /*private void Update()
    {
        Debug.Log(ID);
    }*/

    public void In(int iD, string name, Sprite icon, string description)
    {
        ID = iD;
        Name = name;
        Icon = icon;
        Description = description;
    }

    public void f()
    {
        if (ID == 1)
        {

        }
        else if (ID == 2)
        {

        }
    }
}