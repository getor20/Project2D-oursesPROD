using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerMove playerMove;

    public void OnMove(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        playerMove.SetDirection(input);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        playerMove.SetRunning(context.ReadValueAsButton());
    }
}
