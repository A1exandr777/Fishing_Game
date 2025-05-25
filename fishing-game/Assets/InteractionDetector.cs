using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange; // Closest interactable object in range
    public GameObject InteractionIcon;

    private void Start()
    {
        InteractionIcon.SetActive(false);
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        // interactableInRange?.Interact();
        if (context.performed)
        {
            // Debug.Log(interactableInRange);
            interactableInRange?.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            InteractionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange )
        {
            interactableInRange = null;
            InteractionIcon.SetActive(false);
        }
    }

}
