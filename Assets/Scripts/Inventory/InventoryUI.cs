using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject contentGO;

    public ItemBlueprint itemBlueprint;

    void Start()
    {
        UpdateInventory();
    }

    void UpdateInventory()
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
