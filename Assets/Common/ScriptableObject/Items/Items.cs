using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Items : ScriptableObject
{
    public int ID;
    public string Name;
    public Sprite Icon;
    public string Description;
}   