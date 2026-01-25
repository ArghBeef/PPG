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
}
