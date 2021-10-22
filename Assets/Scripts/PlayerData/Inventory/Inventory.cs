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
}

[System.Serializable]
public class ItemSlot
{
    public InventoryItem item;
    public int count;
}
