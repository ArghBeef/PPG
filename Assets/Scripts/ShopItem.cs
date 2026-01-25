using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    public GameObject TooltipPrefab;

    InventoryItem item;
    ShopUI shopUI;
    bool isShopItem;

    GameObject tooltipInstance;
    Canvas rootCanvas;

    float lastClickTime;
    const float DoubleClickTime = 0.3f;

    void Awake()
    {
        rootCanvas = GetComponentInParent<Canvas>();
    }

    public void Setup(InventoryItem newItem, ShopUI ui, bool fromShop)
    {
        item = newItem;
        shopUI = ui;
        isShopItem = fromShop;

        RawImage img = GetComponent<RawImage>();
        if (img != null)
            img.texture = item.icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
        {
            Debug.LogError("ShopItem: item is NULL", this);
            return;
        }
        if (TooltipPrefab == null || rootCanvas == null) return;
        if (tooltipInstance != null) return;

        tooltipInstance = Instantiate(TooltipPrefab, rootCanvas.transform);
        tooltipInstance.transform.position = eventData.position;
        tooltipInstance.GetComponent<ItemTooltip>().Setup(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipInstance != null)
            Destroy(tooltipInstance);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null || shopUI == null) return;

        if (Time.time - lastClickTime < DoubleClickTime)
        {
            if (isShopItem)
                shopUI.Buy(item);
            else
                shopUI.Sell(item);
        }

        lastClickTime = Time.time;
    }

    void OnDisable()
    {
        if (tooltipInstance != null)
        {
            Destroy(tooltipInstance);
            tooltipInstance = null;
        }
    }
}
