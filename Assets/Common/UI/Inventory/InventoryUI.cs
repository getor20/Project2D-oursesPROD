using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _slotParent;
    [SerializeField] private GameObject _slotPrefab;

    private InventorySlot[] _slots;

}