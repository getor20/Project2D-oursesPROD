using Assets.Script.Player;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _inventoryUI;
    [SerializeField] private GameObject _interaction;

    private void Start()
    {
        _inventoryUI.SetActive(false);
        _interaction.SetActive(false);
    }

    private void Update()
    {
        _inventoryUI.SetActive(_playerController.DisplayInventory);
        _interaction.SetActive(_playerController.IsTrigger);
    }
}

