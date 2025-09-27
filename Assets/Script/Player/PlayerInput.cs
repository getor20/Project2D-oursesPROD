using Assets.Script.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerInputData _inputData;

    public bool IsInteraction { get; private set; }
    public bool DisplayInventory { get; private set; }

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        DisplayInventory = false;
    }

    private void Update()
    {
        _playerController.SetInput(_inputData);

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
            DisplayInventory = !DisplayInventory;
            //Debug.Log("Toggle state: " + _displayInventory);
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerController.SetInteraction(true);
            IsInteraction = true;
            //Debug.Log("Interaction performed");
            // Implement interaction logic here
        }
        else if (context.canceled)
        {
            _playerController.SetInteraction(false);
            IsInteraction = false;
            //Debug.Log("Interaction canceled");
            // Implement interaction logic here
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
