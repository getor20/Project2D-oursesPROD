using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PolygonCollider2D _polygonCollider2D;
    [SerializeField] private ShadowCaster2D _shadowCaster2D;

    private void Start()
    {

        // 1. Выбираем случайный спрайт
        Sprite selectedSprite = _sprites[Random.Range(0, _sprites.Length)];
        

        if (_spriteRenderer.sprite == _sprites[0])
        {
            _spriteRenderer.sprite = selectedSprite;
        }
        else if (_spriteRenderer.sprite == _sprites[1]) 
        {
            _spriteRenderer.sprite = selectedSprite;
        }

        UpdateColliderShape(selectedSprite);
    }

    private void UpdateColliderShape(Sprite sprite)
    {
        if (sprite == null || _polygonCollider2D == null) return;

        // Создаем временный список для хранения точек
        List<Vector2> points = new List<Vector2>();

        // Получаем точки первой физической формы (индекс 0)
        sprite.GetPhysicsShape(0, points);

        // Присваиваем эти точки PolygonCollider2D
        _polygonCollider2D.SetPath(0, points);
    }
}