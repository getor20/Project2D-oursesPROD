using Assets.Script.Player;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _inventoryUI;

    private void Start()
    {
        _inventoryUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        _inventoryUI.gameObject.SetActive(_playerController.DisplayInventory);
    }
}

