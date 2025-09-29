using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private Items _item;

    private SpriteRenderer _image;

    public int ID { get; private set; }
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public string Description { get; private set; }

    public bool IsTrigger { get; private set; }

    private void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _image.sprite = _item.Icon;

        ID = _item.ID;
        Name = _item.Name;
        Icon = _item.Icon;
        Description = _item.Description;
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
