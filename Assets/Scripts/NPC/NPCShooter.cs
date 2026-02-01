using UnityEngine;

public class NPCWeapon : MonoBehaviour
{
    public Transform muzzle;
    public GameObject projectilePrefab;
    public float projectileSpeed = 200f;
    public float fireCooldown = 0.5f;

    Transform currentTarget;
    float lastShotTime;

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    public void ClearTarget(Transform target)
    {
        if (currentTarget == target)
            currentTarget = null;
    }

    void Update()
    {
        if (currentTarget == null) return;

        AimAtTarget();

        if (Time.time >= lastShotTime + fireCooldown)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    void AimAtTarget()
    {
        Vector3 dir = currentTarget.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        if (muzzle == null || projectilePrefab == null || currentTarget == null)
            return;

        Vector3 direction = (currentTarget.position - muzzle.position).normalized;

        GameObject proj = Instantiate(
            projectilePrefab,
            muzzle.position,
            Quaternion.LookRotation(direction)
        );

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = direction * projectileSpeed;
    }
}
