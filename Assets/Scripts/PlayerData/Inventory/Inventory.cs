using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float gold;

    public List<ItemSlot> slots;

    public TaskItem drinkWater;

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
                PopupIndicator.OnObtain?.Invoke("inventory","Obtained item");
                slot.count += quantity;
                return;
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

                if(slot.item.itemName == "Water")
                {
                    if (SpecialCounters.Instance.IncreaseCounterAndCheck("DrinkWater"))
                    {
                        Tasks.Instance.CompleteTask(drinkWater);
                    }
                }
                return;
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

    public bool IsEmpty(InventoryItem item)
    {
        foreach (ItemSlot slot in slots)
        {
            if (item == slot.item)
            {
                if (slot.count <= 0)
                    return true;
                else
                    return false;
            }
        }
        Debug.Log("CheckEmpty method error");
        return false;
    }

    public void ResetData()
    {
        gold = 1000;

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
