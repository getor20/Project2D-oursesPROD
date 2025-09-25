using Assets.Script.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerInputData _inputData;

    public bool _displayInventory { get; private set; }

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
        /*context.control.displayName;
        context.control.name;
        context.control.device;*/
        /*var i = context.action.name;
        if (i == "Wove")
        {
            //Debug.Log("Кнопка нажата: " + context.action.name);
        }*/

        _inputData.MoveDirection = context.ReadValue<Vector2>();
        //_playerController.SetDirection(input);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _inputData.IsRunning = context.ReadValueAsButton();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _displayInventory = !_displayInventory;
            //Debug.Log("Toggle state: " + _displayInventory);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inputData.IsAttacking = true;
        }
    }
    
}
