using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject inventoryPanel;
    public Transform itemsParent;
    public GameObject slotPrefab;
    public TMP_Text goldText;

    public InputActionReference inventoryAction;

    void OnEnable()
    {
        inventoryAction.action.Enable();
        inventoryAction.action.performed += OnInventoryPressed;
    }

    void OnDisable()
    {
        inventoryAction.action.performed -= OnInventoryPressed;
        inventoryAction.action.Disable();
    }

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void OnInventoryPressed(InputAction.CallbackContext ctx)
    {
        ToggleInventory();
    }

    void ToggleInventory()
    {
        bool isOpen = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isOpen);

        Cursor.lockState = !isOpen
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = !isOpen;

        if (!isOpen)
            RefreshUI();
    }

    void RefreshUI()
    {
        foreach (Transform child in itemsParent)
            Destroy(child.gameObject);

        foreach (InventoryItem item in inventory.items)
        {
            GameObject slot = Instantiate(slotPrefab, itemsParent);
            slot.GetComponent<RawImage>().texture = item.icon;
        }

        goldText.text = "Gold: " + inventory.gold;
    }
}
