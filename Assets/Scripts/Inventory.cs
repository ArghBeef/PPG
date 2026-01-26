using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int gold;

    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(InventoryItem item)
    {
        items.Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        items.Remove(item);
    }

    public void UseItem(InventoryItem item)
    {
        switch (item.pickupType)
        {
            case PickupType.Medkit:
                GetComponent<PlayerStats>().ChangeHealth(item.value);
                break;

            case PickupType.Stamina:
                GetComponent<PlayerStats>().stamina += item.value;
                break;

            case PickupType.Ammo:
                GetComponent<PlayerStats>().ChangeAmmo(item.value);
                break;
        }
    }
}
