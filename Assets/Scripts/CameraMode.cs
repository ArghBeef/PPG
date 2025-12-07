using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMode : MonoBehaviour
{
    public InputActionReference toggleAction;
    public Camera mainCamera;
    public Transform anchorFP;
    public Transform anchorRTS;

    CameraFP camFP;
    CameraRTS camRTS;
    bool usingRTS;

    void Awake()
    {
        camFP = mainCamera.GetComponent<CameraFP>();
        camRTS = mainCamera.GetComponent<CameraRTS>();
    }

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
        SwitchToFP();
    }

    void OnToggle(InputAction.CallbackContext ctx)
    {
        if (usingRTS) SwitchToFP();
        else SwitchToRTS();
    }

    void SwitchToFP()
    {
        usingRTS = false;

        if (mainCamera != null && anchorFP != null)
        {
            mainCamera.transform.SetParent(anchorFP);
            mainCamera.transform.localPosition = Vector3.zero;
            mainCamera.transform.localRotation = Quaternion.identity;
        }

        if (camFP != null) camFP.enabled = true;
        if (camRTS != null) camRTS.enabled = false;
    }

    void SwitchToRTS()
    {
        usingRTS = true;

        if (mainCamera != null && anchorRTS != null)
        {
            mainCamera.transform.SetParent(null);
            mainCamera.transform.position = anchorRTS.position;
            mainCamera.transform.rotation = anchorRTS.rotation;
        }

        if (camFP != null) camFP.enabled = false;
        if (camRTS != null) camRTS.enabled = true;
    }
}
