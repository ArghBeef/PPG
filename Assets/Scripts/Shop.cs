using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
    public GameObject InvParent;
    public GameObject ShopParent;
    public GameObject MainUI;
    public InputActionReference Interact;

    bool inRange;
    bool open;

    void Awake()
    {
        if (Interact == null)
            return;

        Interact.action.Enable();
        Interact.action.performed += ctx =>
        {
            if (!inRange)
                return;

            Toggle();
        };
    }

    void OnDestroy()
    {
        if (Interact != null)
            Interact.action.performed -= ctx => Toggle();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        inRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        inRange = false;

        if (ShopParent.activeSelf)
            CloseShop();
    }

    void Toggle()
    {
        if (ShopParent.activeSelf)
            CloseShop();
        else
            OpenShop();
    }

    void OpenShop()
    {
        open = true;

        MainUI.SetActive(false);
        InvParent.SetActive(true);
        ShopParent.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    void CloseShop()
    {
        open = false;

        ShopParent.SetActive(false);
        InvParent.SetActive(false);
        MainUI.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
