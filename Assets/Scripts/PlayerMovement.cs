using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference crouchAction;

    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    public float standHeight = 2f;
    public float crouchHeight = 1f;

    Rigidbody rb;
    CapsuleCollider col;
    bool crouch;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        crouchAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        crouchAction.action.Disable();
    }

    void Update()
    {
        if (jumpAction.action.triggered && IsGrounded())
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);

        crouch = crouchAction.action.IsPressed();
        if (crouch)
            col.height = crouchHeight;
        else
            col.height = standHeight;
    }

    void FixedUpdate()
    {
        Vector2 i = moveAction.action.ReadValue<Vector2>();
        Vector3 moveLocal = new Vector3(i.x, 0f, i.y);
        Vector3 moveWorld = transform.TransformDirection(moveLocal) * moveSpeed;

        Vector3 v = rb.linearVelocity;
        v.x = moveWorld.x;
        v.z = moveWorld.z;
        rb.linearVelocity = v;
    }

    bool IsGrounded()
    {
        float dist = col.bounds.extents.y + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, dist);
    }
}
