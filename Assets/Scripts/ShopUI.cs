using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public Inventory PlayerInventory;
    public InventoryUI InventoryUI;

    public GameObject ShopParent;
    public Transform ShopPanel;
    public Transform InvPanel;

    public TMP_Text Gold;
    public GameObject InventorySlot;

    public InventoryItem[] ShopItems;

    void Awake()
    {
        ShopParent.SetActive(false);
    }
    void OnEnable()
    {
        Refresh();
    }

    public void OpenShop()
    {
        Refresh();
        ShopParent.SetActive(true);
    }

    public void CloseShop()
    {
        Refresh();
        ShopParent.SetActive(false);
    }

    public void Buy(InventoryItem item)
    {
        if (PlayerInventory.gold < item.price) return;

        PlayerInventory.gold -= item.price;

        InventoryItem newItem = new InventoryItem
        {
            itemName = item.itemName,
            icon = item.icon,
            value = item.value,
            price = item.price,
            pickupType = item.pickupType
        };

        PlayerInventory.AddItem(newItem);

        Refresh();
        InventoryUI.Refresh();
    }

    public void Sell(InventoryItem item)
    {
        PlayerInventory.gold += item.price;
        PlayerInventory.RemoveItem(item);

        Refresh();
        InventoryUI.Refresh();
    }

    void Refresh()
    {
        Gold.text = PlayerInventory.gold.ToString();

        Clear(ShopPanel);
        Clear(InvPanel);

        foreach (InventoryItem item in ShopItems)
            Create(item, ShopPanel, true);

        foreach (InventoryItem item in PlayerInventory.items)
            Create(item, InvPanel, false);
    }

    void Create(InventoryItem item, Transform parent, bool buy)
    {
        GameObject obj = Instantiate(InventorySlot, parent);

        ShopItem ui = obj.GetComponent<ShopItem>();
        ui.Setup(item, this, buy);
    }

    void Clear(Transform parent)
    {
        foreach (Transform c in parent)
            Destroy(c.gameObject);
    }
}
