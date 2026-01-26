using UnityEngine;

public class NPCShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    void Start()
    {
        InvokeRepeating(nameof(Shoot), 1, fireRate);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
