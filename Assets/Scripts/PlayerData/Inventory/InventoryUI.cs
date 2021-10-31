using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject contentGO;

    public ItemBlueprint itemBlueprint;

    public TextMeshProUGUI itemDescription;

    public InventoryItem selectedItem;

    public static InventoryUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        UpdateInventoryUI();
    }

    private void OnDisable()
    {
        selectedItem = null;
        itemDescription.text = "";
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in contentGO.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemSlot slot in Inventory.Instance.slots)
        {
            if (slot.count == 0)
                continue;

            ItemBlueprint itemBlueprintSlot = Instantiate(itemBlueprint, contentGO.transform);
            itemBlueprintSlot.SetData(slot);
        }
    }

    public void UseItem()
    {
        Inventory.Instance.ConsumeItem(selectedItem);
    }
}
