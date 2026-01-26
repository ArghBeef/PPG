using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Hotbar : MonoBehaviour
{
    public RawImage[] slots;

    public Inventory inventory;
    public InventoryUI inventoryUI;

    public InputActionReference Hotbar1;
    public InputActionReference Hotbar2;
    public InputActionReference Hotbar3;
    public InputActionReference Hotbar4;
    public InputActionReference Hotbar5;

    InventoryItem[] items = new InventoryItem[5];

    void Awake()
    {
        Bind(Hotbar1, 0);
        Bind(Hotbar2, 1);
        Bind(Hotbar3, 2);
        Bind(Hotbar4, 3);
        Bind(Hotbar5, 4);

        ClearVisuals();
    }

    void Bind(InputActionReference action, int index)
    {
        action.action.Enable();
        action.action.performed += _ => Use(index);
    }

    public bool AddItem(InventoryItem item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                slots[i].texture = item.icon;
                slots[i].color = Color.white;
                return true;
            }
        }

        Debug.Log("Hotbar full");
        return false;
    }

    void Use(int index)
    {
        if (items[index] == null) return;

        InventoryItem item = items[index];

        inventory.UseItem(item);
        inventory.RemoveItem(item);

        items[index] = null;
        slots[index].texture = null;
        slots[index].color = new Color(1, 1, 1, 0);

        inventoryUI.Refresh();
    }

    void ClearVisuals()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].texture = null;
            slots[i].color = new Color(1, 1, 1, 0);
        }
    }
}
