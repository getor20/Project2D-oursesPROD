using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemsStatBlock : ScriptableObject
{
    public int ID;
    public string Name;
    public Sprite Icon;
    public string Description;
    public float Edibility;
    public GameObject PrefabObject; // Префаб для отображения в мире
}   