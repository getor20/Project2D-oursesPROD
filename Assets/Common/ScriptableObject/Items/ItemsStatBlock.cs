using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemsStatBlock : ScriptableObject
{
    public int ID;
    public string Name;
    public Sprite Icon;
    public string Description;
    public int MaxDurability;
}   