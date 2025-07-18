using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInput : MonoBehaviour
{
    private PlayerMove _playerMove;
    public Vector2 input;
    private Vector2 _angularVector = new Vector2(0.707107f, 0.707107f);

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    private void FixedUpdate()
    {
        //DirectionVector();
    }

    public void DirectionVector()
    {
        if (input.y == _angularVector.y && input.x == _angularVector.x)
        {
            input = _angularVector;
            Debug.Log("_vector1" + input);
        }
        else if (input.y == -_angularVector.y && input.x == -_angularVector.x)
        {
            Debug.Log("_vector2" + input);
        }
        else if (input.y == -_angularVector.y && input.x == _angularVector.x)
        {
            Debug.Log("_vector3" + input);
        }
        else if (input.y == _angularVector.y && input.x == -_angularVector.x)
        {
            Debug.Log("_vector4" + input);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var i = context.action.name;
        if (i == "Wove")
        {
            Debug.Log("Кнопка нажата: " + context.action.name);
        }

        input = context.ReadValue<Vector2>();
        _playerMove.SetDirection(input);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _playerMove.SetRunning(context.ReadValueAsButton());
    }
}
