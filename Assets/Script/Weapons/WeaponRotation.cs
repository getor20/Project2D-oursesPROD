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
        }
        else if (right && down)
        {
            centerAngle = -135f;
            _spriteRenderer.sortingOrder = 1;
        }
        else if (left && up)
        {
            centerAngle = 45f;
            _spriteRenderer.sortingOrder = -1;
        }
        else if (left && down)
        {
            centerAngle = 135f;
            _spriteRenderer.sortingOrder = 1;
        }
        else if (right)
        {
            centerAngle = -90f;
            _spriteRenderer.sortingOrder = 1;
        }
        else if (left)
        {
            centerAngle = 90f;
            _spriteRenderer.sortingOrder = 1;
        }
        else if (up)
        {
            centerAngle = 0f;
            _spriteRenderer.sortingOrder = -1;
        }
        else if (down)
        {
            centerAngle = 180f;
            _spriteRenderer.sortingOrder = 1;
        }
        else
        {
            centerAngle = targetAngle;
            deviation = 180f;
        }

        float angleDifference = Mathf.DeltaAngle(centerAngle, targetAngle);
        float clampedDifference = Mathf.Clamp(angleDifference, -deviation, deviation);
        float clampedAngle = centerAngle + clampedDifference;

        transform.rotation = Quaternion.Euler(0, 0, clampedAngle);

        // --- НОВАЯ УПРОЩЕННАЯ ЛОГИКА flipX ---
        // Если спрайт смотрит "влево" (относительно игрока), то переворачиваем.
        // Это означает, что если x-координата вектора от оружия до мыши отрицательна,
        // то мышь находится слева от оружия.

        // Более надежный способ: используем `transform.localEulerAngles.z`
        // или просто `clampedAngle`, но с учетом диапазона от 0 до 360.

        // Получаем угол в диапазоне от 0 до 360 градусов
        float currentZAngle = transform.localEulerAngles.z;

        // Если оружие смотрит вверх (0), влево (90), вниз (180), вправо (270 или -90)
        // Если _rotationOffset = 90, то:
        //  - 0 градусов (localEulerAngles.z) = weapon pointing up
        //  - 90 degrees = weapon pointing left
        //  - 180 degrees = weapon pointing down
        //  - 270 degrees (or -90) = weapon pointing right

        // Мы хотим flipX = true, когда оружие "смотрит" влево.
        // В данном случае, это когда угол находится между 90 и 270 градусами.
        /*if (currentZAngle > 90f && currentZAngle < 270f)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }*/

        // Если это не сработает, то попробуем использовать directionToMouse.x
        if (directionToMouse.x < 0) // Мышь находится слева от оружия
        {
             _spriteRenderer.flipX = true;
        }
        else // Мышь находится справа от оружия
        {
            _spriteRenderer.flipX = false;
        }
    }
}