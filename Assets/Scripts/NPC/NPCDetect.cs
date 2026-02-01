using UnityEngine;

public class NPCDetect : MonoBehaviour
{
    public BoxCollider detectionCollider;

    NPCWeapon weapon;
    string myTag;

    void Awake()
    {
        weapon = GetComponent<NPCWeapon>();
        myTag = gameObject.tag;

        if (weapon == null)
        {
            enabled = false;
            return;
        }

        if (detectionCollider == null)
        {
            enabled = false;
            return;
        }

        detectionCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (myTag == "Enemy" && other.CompareTag("Player"))
        {
            weapon.SetTarget(other.transform);
            return;
        }

        if ((myTag == "Enemy" && other.CompareTag("Friendly")) ||
            (myTag == "Friendly" && other.CompareTag("Enemy")))
        {
            weapon.SetTarget(other.transform.root);
        }
    }

    void OnTriggerExit(Collider other)
    {
        weapon.ClearTarget(other.transform.root);
    }
}
