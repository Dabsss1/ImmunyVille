using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBlueprint : MonoBehaviour
{
    InventoryItem inventoryItem;

    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI quantity;

    public void SetData (ItemSlot item)
    {
        itemIcon.sprite = item.item.itemIcon;
        itemName.text = item.item.itemName;
        quantity.text = $"X: {item.count}";

        inventoryItem = item.item;
    }

    public void SelectItem()
    {
        InventoryUI.Instance.itemDescription.text = inventoryItem.itemDescription;
        InventoryUI.Instance.selectedItem = inventoryItem;
    }
}
