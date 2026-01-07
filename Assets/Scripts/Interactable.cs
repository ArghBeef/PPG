using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    Rigidbody rb;
    bool isInteracting;
    Transform followTarget;

    public float followDistance = 2f;
    public float followSpeed = 15f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void StartInteraction(Transform target)
    {
        isInteracting = true;
        followTarget = target;

        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;

        Debug.Log("Interactable Picked Up");
    }

    public void StopInteraction()
    {
        isInteracting = false;
        followTarget = null;

        rb.useGravity = true;

        Debug.Log("Interactable Droped");
    }

    void FixedUpdate()
    {
        if (!isInteracting || followTarget == null) return;

        Vector3 targetPos = followTarget.position + followTarget.forward * followDistance;
        Vector3 newPos = Vector3.Lerp(rb.position, targetPos, followSpeed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);
    }
}
