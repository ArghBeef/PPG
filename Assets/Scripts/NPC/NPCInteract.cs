using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NPCDialog))]
public class NPCInteract : MonoBehaviour
{
    public InputActionReference InteractAction;

    bool playerInRange;
    NPCDialog dialog;

    void Awake()
    {
        dialog = GetComponent<NPCDialog>();

        InteractAction.action.Enable();
        InteractAction.action.performed += OnInteract;
    }

    void OnDestroy()
    {
        InteractAction.action.performed -= OnInteract;
    }

    void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!playerInRange) return;

        if (DialogSystem.Instance.IsOpen)
            DialogSystem.Instance.CloseDialog();
        else
            dialog.Interact();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
