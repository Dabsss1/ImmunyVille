using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemBlueprint : MonoBehaviour
{
    InventoryItem item;

    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI price;

    public void SetData(InventoryItem item)
    {
        this.item = item;

        icon.sprite = item.itemIcon;
        itemName.text = item.itemName;
        price.text = $"{item.itemPrice}";
    }

    public void BuyItem()
    {
        if (Inventory.Instance.gold >= item.itemPrice)
        {
            Inventory.Instance.gold -= item.itemPrice;
            Inventory.Instance.ObtainItem(item,1);
            ShopUI.Instance.UpdateShopUI();
            ShopUI.Instance.ShowDeductMoney($"-{item.itemPrice}");
        }
        else
        {
            ShopUI.Instance.ShowErrorMoney();
        }
    }
}
