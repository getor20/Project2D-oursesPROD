using UnityEngine;

public class Food : MonoBehaviour
{
    // ⚡ Ссылка на ScriptableObject, а не на другой MonoBehaviour
    [SerializeField] private ItemsStatBlock _data;

    private SpriteRenderer _image;

    // ⚡ Публичное свойство, чтобы Inventory мог получить данные
    public ItemsStatBlock Data => _data;

    public bool IsTrigger { get; private set; }

    private void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (_data != null)
        {
            _image.sprite = _data.Icon;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IsTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        IsTrigger = false;
    }
}