using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string name;
    public Texture2D icon;
    public int value;
    public PickupType pickupType;
}