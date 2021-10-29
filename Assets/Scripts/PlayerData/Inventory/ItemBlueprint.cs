using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBlueprint : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI quantity;

    public string itemDescription;

    public void SetData (ItemSlot item)
    {
        itemIcon.sprite = item.item.itemIcon;
        itemName.text = item.item.itemName;
        quantity.text = $"X: {item.count}";

        itemDescription = item.item.itemDescription;

    }

    public void SelectItem()
    {
        InventoryUI.Instance.itemDescription.text = itemDescription;
    }
}