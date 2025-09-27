using Assets.Script.Player;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private PlayerController _hitboxObject;
    [SerializeField] private Items _item;
    [SerializeField] private Inventory _inventory;
    
    private SpriteRenderer _image;
    private int _index;

    public bool IsInteraction { get; private set; }

    private void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _index = _item.ID;
        _image.sprite = _item.Icon;
    }

    private void Update()
    {
        Interaction();
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        IsInteraction = true;
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        IsInteraction = false;
        
    }

    private void Interaction()
    {
        if (IsInteraction && _hitboxObject.IsInteraction)
        {
            Debug.Log($"Trigger entered by . Interaction successful.");
            Destroy(gameObject);
            // Here you can add code to add the item to the player's inventory
        }
    }
}
