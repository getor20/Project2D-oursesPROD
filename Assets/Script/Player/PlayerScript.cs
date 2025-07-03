using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Vector2 directionVector2 = Vector2.zero;

    [SerializeField]
    private float MainSpeed;
    private float WalkingSpeed = 7f;   
    private float RunSpeed = 11f;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        MainSpeed = WalkingSpeed;
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = directionVector2 * MainSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        directionVector2 = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            MainSpeed = RunSpeed;
        }
        else if (context.canceled)
        {
            MainSpeed = WalkingSpeed;
        }
    }
}
