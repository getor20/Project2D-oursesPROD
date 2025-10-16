using Assets.Script.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerInputData _inputData;

    private bool _displayInventory;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        _playerController.SetInput(_inputData);
        _inputData.IsAttacking = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _inputData.MoveDirection = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _inputData.IsRunning = context.ReadValueAsButton();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        _playerController.SetInventory(_displayInventory = !_displayInventory);
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        _playerController.SetInteraction(context.performed);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inputData.IsAttacking = true;
        }
    }

    public void OnDropItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerController.SetDropItem(/*true*/);
        }
    }
}
