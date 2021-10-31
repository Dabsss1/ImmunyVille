using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> slots;

    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ObtainItem(InventoryItem item, int quantity)
    {
        foreach (ItemSlot slot in slots)
        {
            if (item == slot.item)
            {
                slot.count += quantity;
            }
        }
    }

    public void ConsumeItem(InventoryItem item)
    {
        foreach (ItemSlot slot in slots)
        {
            if (item == slot.item)
            {
                if (slot.count <= 0)
                    return;
                slot.count --;
                DistributeStats(item);
                InventoryUI.Instance.UpdateInventoryUI();
            }
        }
    }

    void DistributeStats(InventoryItem item)
    {
        Stats.Instance.body += item.body;
        Stats.Instance.health += item.health;
        Stats.Instance.confidence += item.confidence;
        Stats.Instance.strength += item.strength;

        HungerThirst.Instance.IncreaseHunger(item.hunger);
        HungerThirst.Instance.IncreaseThirst(item.thirst);
    }

    public void ResetData()
    {
        foreach (ItemSlot slot in slots)
        {
            slot.count = 0;
        }
    }
}

[System.Serializable]
public class ItemSlot
{
    public InventoryItem item;
    public int count;
}
