using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPointClickMovement : MonoBehaviour
{
    public InputActionReference rightClickAction; // Button
    public Camera cameraRef;
    public float moveSpeed = 5f;
    public float stopDistance = 0.2f;

    Rigidbody rb;
    Vector3? target;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        if (rightClickAction != null) rightClickAction.action.Enable();
    }

    void OnDisable()
    {
        if (rightClickAction != null) rightClickAction.action.Disable();
    }

    void Update()
    {
        if (rightClickAction != null && rightClickAction.action.triggered && cameraRef != null)
        {
            Ray ray = cameraRef.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                target = hit.point;
            }
        }
    }

    void FixedUpdate()
    {
        if (!target.HasValue) return;

        Vector3 dir = target.Value - transform.position;
        dir.y = 0f;
        float d = dir.magnitude;

        if (d < stopDistance)
        {
            rb.linearVelocity = Vector3.zero;
            target = null;
            return;
        }

        dir.Normalize();
        rb.linearVelocity = dir * moveSpeed;
        transform.forward = dir;
    }
}
