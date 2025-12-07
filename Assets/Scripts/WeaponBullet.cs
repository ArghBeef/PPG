using UnityEngine;

public class WeaponBullet : MonoBehaviour
{
    public bool destroyImmediatelyOnHit = true;
    public float delayAfterHit = 5f;
    public float damage = 15f;

    public float maxLifeTime = 10f;

    public GameObject hitEffectPrefab;
    public AudioClip hitSound;
    public float hitSoundVolume = 1f;

    bool hasHit;

    void Start()
    {
        if (maxLifeTime > 0f)
            Destroy(gameObject, maxLifeTime);

        
    }

    void OnCollisionEnter(Collision collision)
    {

        Vector3 hitPoint = transform.position;
        if (collision.contacts.Length > 0)
            hitPoint = collision.contacts[0].point;

        if (hitEffectPrefab != null)
        {
            GameObject fx = Instantiate(hitEffectPrefab, hitPoint, Quaternion.identity);
            Destroy(fx, 3f);
        }

        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, hitPoint, hitSoundVolume);

        collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);

        if (destroyImmediatelyOnHit)
        {
            Destroy(gameObject);
        }
        else
        {
            if (!hasHit)
            {
                hasHit = true;

                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.isKinematic = true;
                }

                Collider col = GetComponent<Collider>();
                if (col != null)
                    col.enabled = false;

                Destroy(gameObject, delayAfterHit);
            }
        }
    }
}
