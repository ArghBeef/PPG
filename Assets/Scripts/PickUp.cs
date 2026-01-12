using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    public PickupType pickupType;
    public int value = 10;

    public bool storeInInventory;
    public InventoryItem inventoryItem;

    public AudioClip pickupSound;

    void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponent<PlayerStats>();
        Inventory inventory = other.GetComponent<Inventory>();

        if (stats == null && inventory == null) return;

        if (storeInInventory)
        {
            if (inventory == null || inventoryItem == null) return;

            inventory.AddItem(inventoryItem);
        }
        else
        {
            ApplyInstantPickup(stats, inventory);
        }

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        Destroy(gameObject);
    }

    void ApplyInstantPickup(PlayerStats stats, Inventory inventory)
    {
        switch (pickupType)
        {
            case PickupType.Health:
                stats?.ChangeHealth(value);
                break;

            case PickupType.Ammo:
                stats?.ChangeAmmo(value);
                break;

            case PickupType.Damage:
                stats?.ChangeHealth(-value);
                break;

            case PickupType.Stamina:
                if (stats != null)
                {
                    stats.stamina = Mathf.Clamp(stats.stamina + value, 0, stats.maxStamina);
                }
                break;

            case PickupType.Gold:
                inventory.gold += value;
                break;
        }
    }
}
