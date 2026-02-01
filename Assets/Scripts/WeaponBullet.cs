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

    void OnTriggerEnter(Collider other)
    {
        Vector3 hitPoint = transform.position;

        if (hitEffectPrefab != null)
        {
            GameObject fx = Instantiate(hitEffectPrefab, hitPoint, Quaternion.identity);
            Destroy(fx, 3f);
        }

        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, hitPoint, hitSoundVolume);

        other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);

        Destroy(gameObject);
    }
}
