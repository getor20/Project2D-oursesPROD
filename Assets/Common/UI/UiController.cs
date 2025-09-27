using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private GameObject _inventoryUI;


    private void Start()
    {
        _inventoryUI.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        _inventoryUI.gameObject.SetActive(_playerInput.DisplayInventory);
    }
}

