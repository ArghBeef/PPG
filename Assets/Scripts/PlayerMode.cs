using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMode : MonoBehaviour
{
    public InputActionReference toggleAction;
    public MonoBehaviour playerMovement;
    public MonoBehaviour pointClickMovement;

    bool usingPointClick;

    void OnEnable()
    {
        toggleAction.action.Enable();
        toggleAction.action.performed += OnToggle;
    }

    void OnDisable()
    {
        toggleAction.action.performed -= OnToggle;
        toggleAction.action.Disable();
    }

    void Start()
    {
        SetMode(false);
    }

    void OnToggle(InputAction.CallbackContext ctx)
    {
        usingPointClick = !usingPointClick;
        SetMode(usingPointClick);
    }

    void SetMode(bool pointClick)
    {
        if (playerMovement != null) playerMovement.enabled = !pointClick;
        if (pointClickMovement != null) pointClickMovement.enabled = pointClick;
    }
}
