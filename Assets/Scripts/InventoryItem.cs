using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public Texture2D icon;
    public int value;
    public int price;
    public PickupType pickupType;
}
