using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2D => GetComponent<Rigidbody2D>();    
    private Vector2 vector2 = Vector2.zero;

    [SerializeField]
    private float speed = 7f;
    private float run = 4f;
    public bool move = false;

    private void FixedUpdate()
    {
        rd2D.velocity = new Vector2(vector2.x * speed, vector2.y * speed);
        if ((vector2.x > 0 && transform.localScale.x < 0) || (vector2.x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            move = true;
        }
        else if (context.canceled)
        {
            move = false;
        }

        vector2 = context.ReadValue<Vector2>();
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speed += run;
        }
        else if (context.canceled)
        {
            speed -= run;
        }
    }
}
