using Assets.Script.Player;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryUI;

    private PlayerController _playerController;

    private void Awake()
    {
       _playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        _inventoryUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        _inventoryUI.gameObject.SetActive(_playerController.DisplayInventory);
    }
}

