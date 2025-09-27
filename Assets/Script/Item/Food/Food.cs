using Assets.Script.Player;
using UnityEngine;

public class Food : MonoBehaviour
{
    //[SerializeField] private LayerMask _target;
    [SerializeField] private PlayerController _hitboxObject;
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private Items _item;

    private SpriteRenderer _image;
    private CapsuleCollider2D _collider2D;
    private bool _isInteraction;

    private void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        _image.sprite = _item.Icon;
    }

    private void FixedUpdate()
    {
        // Debug.Log(_hitboxObject.IsInteraction);
        /*if (_isInteraction && _hitboxObject.IsInteraction)
        {
            Debug.Log($"Trigger entered by . SSSSSSSSSSSSSSSS");
        }*/
        Interaction();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /*if (_playerInput != null && _playerInput.IsInteraction)
        {
            Debug.Log($"Trigger entered by {collider.gameObject.name}. SSSSSSSSSSSSSSSS");
        }*/
        _isInteraction = true;
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _isInteraction = false;
        
    }

    private void Interaction()
    {
        if (_isInteraction && _hitboxObject.IsInteraction)
        {
            Debug.Log($"Trigger entered by . Interaction successful.");
            // Here you can add code to add the item to the player's inventory
        }
    }
}
