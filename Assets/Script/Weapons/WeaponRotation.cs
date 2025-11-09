using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] private float _rotationOffset = 90;
    [SerializeField] private float _maxDeviation = 90;

    private SpriteRenderer _spriteRenderer;

    private Vector3 _mousePos;
    private Vector2 _direction;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        RotateWeapon();
    }

    public void SetRotationDirection(Vector2 direction)
    {
        _mousePos = direction;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void RotateWeapon()
    {
        Vector2 directionToMouse = _mousePos - transform.position;

        float targetAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - _rotationOffset;

        float centerAngle = 0f;
        float deviation = _maxDeviation;

        // ... (твой существующий код для centerAngle и deviation, без изменений) ...
        bool right = _direction.x > 0.1f;
        bool left = _direction.x < -0.1f;
        bool up = _direction.y > 0.1f;
        bool down = _direction.y < -0.1f;

        if (right && up)
        {
            centerAngle = -45f;
            _spriteRenderer.sortingOrder = -1;
            _spriteRenderer.flipX = false;
        }
        else if (right && down)
        {
            centerAngle = -135f;
            _spriteRenderer.sortingOrder = 1;
            _spriteRenderer.flipX = false;
        }
        else if (left && up)
        {
            centerAngle = 45f;
            _spriteRenderer.sortingOrder = -1;
            _spriteRenderer.flipX = true;
        }
        else if (left && down)
        {
            centerAngle = 135f;
            _spriteRenderer.sortingOrder = 1;
            _spriteRenderer.flipX = true;
        }
        else if (right)
        {
            centerAngle = -90f;
            _spriteRenderer.sortingOrder = 1;
            _spriteRenderer.flipX = false;
        }
        else if (left)
        {
            centerAngle = 90f;
            _spriteRenderer.sortingOrder = 1;
            _spriteRenderer.flipX = true;
        }
        else if (up)
        {
            centerAngle = 0f;
            _spriteRenderer.sortingOrder = -1;
            if (directionToMouse.x > 0) // Мышь находится справа
            {
                _spriteRenderer.flipX = true;
            }
            else // Мышь находится слева
            {
                _spriteRenderer.flipX = false;
            }
        }
        else if (down)
        {
            centerAngle = 180f;
            _spriteRenderer.sortingOrder = 1;
            if (directionToMouse.x < 0) // Мышь находится слева
            {
                _spriteRenderer.flipX = true;
            }
            else // Мышь находится справа
            {
                _spriteRenderer.flipX = false;
            }
        }
        else
        {
            centerAngle = targetAngle;
            deviation = 180f;
            if (directionToMouse.x < 0) // Мышь находится слева от оружия
            {
                _spriteRenderer.flipX = true;
            }
            else // Мышь находится справа от оружия
            {
                _spriteRenderer.flipX = false;
            }

        }

        float angleDifference = Mathf.DeltaAngle(centerAngle, targetAngle);
        float clampedDifference = Mathf.Clamp(angleDifference, -deviation, deviation);
        float clampedAngle = centerAngle + clampedDifference;

        transform.rotation = Quaternion.Euler(0, 0, clampedAngle);
        
    }
}