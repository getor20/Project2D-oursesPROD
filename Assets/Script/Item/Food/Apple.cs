using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private Items _item;

    private SpriteRenderer _image;

    private void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _image.sprite = _item.Icon;
    }
}
