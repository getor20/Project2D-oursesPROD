using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2D => GetComponent<Rigidbody2D>();    
    private Vector2 vector2 = Vector2.zero;

    [SerializeField]
    private float speed = 10f;

    private void FixedUpdate()
    {
        rd2D.velocity = new Vector2(vector2.x * speed, vector2.y * speed);
    }
    
    public void Muve(InputAction.CallbackContext context)
    {
        if ((vector2.x > 0 && transform.localScale.x < 0) || (vector2.x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        vector2 = context.ReadValue<Vector2>();
    }

}
