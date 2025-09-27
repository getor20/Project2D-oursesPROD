using Assets.Script.Player;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Food> _items = new List<Food>();

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();

    }
    private void Update()
    {
        Interaction();
    }
    private void Interaction()
    {
        if ( _playerController.IsInteraction)
        {
            Debug.Log($"Trigger entered by . Interaction successful.");
            //Destroy(gameObject._items);
            // Here you can add code to add the item to the player's inventory
        }
    }
}
