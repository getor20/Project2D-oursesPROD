using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
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

    public void SetDirection(Vector2 direction)
    {
        // Нормализуем направление
        _direction = direction.normalized;
    }

    private void RotateWeapon()
    {
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = _mousePos - transform.position;

        // Угол в градусах с учетом смещения
        float targetAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - _rotationOffset;

        float centerAngle = 0f;
        float deviation = _maxDeviation; // Используем настраиваемое отклонение

        // 2. Определение центрального угла (centerAngle) для 8 направлений

        // Горизонтальное движение (X)
        bool right = _direction.x > 0.1f;
        bool left = _direction.x < -0.1f;

        // Вертикальное движение (Y)
        bool up = _direction.y > 0.1f;
        bool down = _direction.y < -0.1f;

        if (right && up)
        {
            // Диагональ Вправо-Вверх
            centerAngle = -45f;
        }
        else if (right && down)
        {
            // Диагональ Вправо-Вниз
            centerAngle = -135f;
        }
        else if (left && up)
        {
            // Диагональ Лево-Вверх
            centerAngle = 45f;
        }
        else if (left && down)
        {
            // Диагональ Лево-Вниз
            centerAngle = 135f;
        }
        else if (right)
        {
            // Вправо
            centerAngle = -90f;
        }
        else if (left)
        {
            // Влево
            centerAngle = 90f;
        }
        else if (up)
        {
            // Вверх
            centerAngle = 0f;
        }
        else if (down)
        {
            // Вниз
            centerAngle = 180f;
        }
        else
        {
            // Персонаж стоит: разрешаем полный поворот
            centerAngle = targetAngle; // Целимся прямо в мышь
            deviation = 180f;          // Максимально широкий диапазон
        }

        // 3. Ограничение угла с помощью Mathf.DeltaAngle

        // Если персонаж стоит, мы уже установили centerAngle = targetAngle, 
        // поэтому нет необходимости в Clamp, но мы все равно это делаем для консистентности.
        float angleDifference = Mathf.DeltaAngle(centerAngle, targetAngle);

        // Ограничиваем эту разницу
        float clampedDifference = Mathf.Clamp(angleDifference, -deviation, deviation);

        // Применяем ограниченную разницу к центру
        float clampedAngle = centerAngle + clampedDifference;

        // 4. Применение вращения
        transform.rotation = Quaternion.Euler(0, 0, clampedAngle);
    }
}