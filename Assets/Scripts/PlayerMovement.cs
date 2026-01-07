using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference crouchAction;
    public InputActionReference sprintAction;

    public float moveSpeed = 5f;
    public float jumpHeight = 5f;
    public float standHeight = 2f;
    public float crouchHeight = 1f;

    public float slowSpeedMult = 0.5f;
    public float speedSpeedMult = 1.5f;

    public PlayerWeapon playerWeaponRef;

    Rigidbody rb;
    CapsuleCollider col;
    float speed;
    bool crouch;
    bool sprint;

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

        speed = moveSpeed;

        crouch = crouchAction.action.IsPressed();
        if (crouch)
        {
            col.height = crouchHeight;
            speed *= slowSpeedMult;
        }
        else
            col.height = standHeight;

        sprint = sprintAction.action.IsPressed();
        if(sprint)
            speed *= speedSpeedMult;
    }

    void FixedUpdate()
    {
        Vector2 i = moveAction.action.ReadValue<Vector2>();
        Vector3 moveLocal = new Vector3(i.x, 0f, i.y);


        if (playerWeaponRef != null && playerWeaponRef.isAiming)
            speed *= slowSpeedMult;


        Vector3 moveWorld = transform.TransformDirection(moveLocal) * speed;

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
