using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 3f;

    public InputActionReference interactAction;

    Interactable currentHover;
    Interactable currentHeld;

    void OnEnable()
    {
        if (interactAction != null)
        {
            interactAction.action.Enable();
            interactAction.action.started += OnInteractPressed;
            interactAction.action.canceled += OnInteractReleased;
        }
    }

    void OnDisable()
    {
        if (interactAction != null)
        {
            interactAction.action.started -= OnInteractPressed;
            interactAction.action.canceled -= OnInteractReleased;
            interactAction.action.Disable();
        }
    }

    void Update()
    {
        DetectInteractable();
    }

    void DetectInteractable()
    {
        if (currentHeld != null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Interactable obj = hit.collider.GetComponent<Interactable>();

            if (obj != null)
            {
                currentHover = obj;
                return;
            }
        }

        currentHover = null;
    }

    void OnInteractPressed(InputAction.CallbackContext ctx)
    {
        if (currentHover == null || currentHeld != null) return;

        currentHeld = currentHover;
        currentHeld.StartInteraction(playerCamera.transform);
    }

    void OnInteractReleased(InputAction.CallbackContext ctx)
    {
        if (currentHeld == null) return;

        currentHeld.StopInteraction();
        currentHeld = null;
    }
}
