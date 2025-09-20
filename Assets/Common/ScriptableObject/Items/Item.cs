using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public string Description;
}   