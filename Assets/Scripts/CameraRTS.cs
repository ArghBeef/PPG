using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRTS : MonoBehaviour
{
    public InputActionReference moveAction;
    public float moveSpeed = 15f;

    void OnEnable()
    {
        moveAction.action.Enable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        moveAction.action.Disable();
    }

    void Update()
    {
        Vector2 move = moveAction.action.ReadValue<Vector2>();
        Vector3 moveVec = new Vector3(move.x, 0f, move.y) * moveSpeed * Time.deltaTime;
        transform.Translate(moveVec, Space.World);
    }
}
