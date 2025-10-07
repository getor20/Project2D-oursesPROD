using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private LayerMask _item;
    private void OnTriggerEnter2D(Collider2D collider)
    { 
        if (collider != gameObject)
        {
            // Check if the collider's layer is included in the _item LayerMask
            if (((1 << collider.gameObject.layer) & _item) != 0)
                Debug.Log($"Trigger entered by {collider.name}. Interaction successful.");
        }
    }
}
