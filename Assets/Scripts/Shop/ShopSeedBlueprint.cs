using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSeedBlueprint : MonoBehaviour
{
    PlantItem seed;

    public Image icon;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI price;

    public void SetData(PlantItem seed)
    {
        this.seed = seed;

        icon.sprite = seed.seed;
        itemName.text = seed.plantName;
        price.text = $"{seed.seedPrice}";
    }

    public void BuySeed()
    {
        if (Inventory.Instance.gold >= seed.seedPrice)
        {
            Inventory.Instance.gold -= seed.seedPrice;
            Plants.Instance.ObtainSeed(seed,1);
            ShopUI.Instance.UpdateShopUI();
            ShopUI.Instance.ShowDeductMoney($"-{seed.seedPrice}");
        }
        else
        {
            ShopUI.Instance.ShowErrorMoney();
        }
    }
}
