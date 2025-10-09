using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemsStatBlock : ScriptableObject
{
    public int ID;
    public string Name;
    public Sprite Icon;
    public string Description;
}   