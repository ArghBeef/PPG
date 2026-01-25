using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    public TMP_Text NameText;
    public TMP_Text PriceText;

    public void Setup(InventoryItem item)
    {
        NameText.text = item.itemName;
        PriceText.text = item.price.ToString();
    }
}
