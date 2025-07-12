using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerMove _playerMove;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        _playerMove.SetDirection(input);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _playerMove.SetRunning(context.ReadValueAsButton());
    }
}
