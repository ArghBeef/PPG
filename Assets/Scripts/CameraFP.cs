using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraFP : MonoBehaviour
{
    public InputActionReference lookAction;
    public InputActionReference zoomAction;
    public Transform playerBody;

    public float sensitivity = 2f;
    public float minPitch = -85f;
    public float maxPitch = 85f;
    public float minFOV = 40f;
    public float maxFOV = 80f;
    public float zoomSpeed = 5f;

    float pitch;
    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void OnEnable()
    {
        lookAction.action.Enable();
        zoomAction.action.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        lookAction.action.Disable();
        zoomAction.action.Disable();
    }

    void Update()
    {
        Vector2 look = lookAction.action.ReadValue<Vector2>();
        pitch -= look.y * sensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        if (playerBody != null)
            playerBody.Rotate(Vector3.up * look.x * sensitivity);

        float scroll = zoomAction.action.ReadValue<Vector2>().y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            float fov = cam.fieldOfView + (-scroll) * zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(fov, minFOV, maxFOV);
        }
    }
}
