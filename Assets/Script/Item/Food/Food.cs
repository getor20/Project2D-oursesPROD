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

    private void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        _image.sprite = _item.Icon;
    }

    private void Update()
    {
        // Debug.Log(_hitboxObject.IsInteraction);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /*Debug.Log(_hitboxObject.IsInteraction);
        //int otherObjectLayer = collider.gameObject.layer;
        if (_hitboxObject.IsInteraction == true)
        {
            Debug.Log($"Trigger entered by {collider.gameObject.name}. SSSSSSSSSSSSSSSS");
        }
        if (_target == (_target | (1 << otherObjectLayer)))
        {
            Debug.Log($"{_target}");

        }*/

        //Debug.Log("Interaction component is missing or _hitboxObject is not assigned.");

        if (_hitboxObject == null)
        {
            Debug.LogError("Interaction component is missing or _hitboxObject is not assigned.");
            return; // Stop execution to prevent NullReferenceException
        }

        // NOW you can correctly access the property on the component:
        //Debug.Log(_hitboxObject.IsInteraction);

        if (_playerInput != null && _playerInput.IsInteraction)
        {
            Debug.Log($"Trigger entered by {collider.gameObject.name}. SSSSSSSSSSSSSSSS");
        }
    }
}
