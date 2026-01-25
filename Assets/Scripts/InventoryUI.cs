using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    public GameObject InvParent;
    public Transform InvPanel;

    public GameObject InventorySlot;
    public TMP_Text Gold;

    public InputActionReference InventoryAction;

    void Awake()
    {
        InventoryAction.action.Enable();
        InventoryAction.action.performed += ctx => Toggle();

        InvParent.SetActive(false);
    }

    void Toggle()
    {
        bool open = !InvParent.activeSelf;
        InvParent.SetActive(open);

        if (open)
            Refresh();
    }

    public void Refresh()
    {
        foreach (Transform c in InvPanel)
            Destroy(c.gameObject);

        ShopUI shopUI = FindObjectOfType<ShopUI>();
        if (shopUI == null)
        {
            return;
        }

        foreach (InventoryItem item in inventory.items)
        {
            GameObject slot = Instantiate(InventorySlot, InvPanel);

            slot.GetComponent<RawImage>().texture = item.icon;

            ShopItem shopItem = slot.GetComponent<ShopItem>();
            if (shopItem == null)
            {
                continue;
            }

            shopItem.Setup(item, shopUI, false);
        }

        Gold.text = inventory.gold.ToString();
    }
}
