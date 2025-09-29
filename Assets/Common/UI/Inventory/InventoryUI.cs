using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _slotParent;
    [SerializeField] private GameObject _slotPrefab;

    private InventorySlot[] _slots;

    private void Start()
    {
        Debug.Log($"InventoryUI: Найдено слотов: {_inventory._selectedItemIndex}");
    }


}