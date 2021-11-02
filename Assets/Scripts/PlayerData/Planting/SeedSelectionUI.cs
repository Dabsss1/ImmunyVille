using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSelectionUI : MonoBehaviour
{
    public SeedUIBlueprint plantBlueprint;
    public Transform contentGO;

    private void Start()
    {
        UpdateSeedList();
    }
    private void OnEnable()
    {
        UpdateSeedList();
    }

    void UpdateSeedList()
    {
        foreach (Transform child in contentGO.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (SeedSlot slot in Plants.Instance.seedSlots)
        {
            if (slot.quantity == 0)
                continue;

            SeedUIBlueprint plantBlueprintSlot = Instantiate(plantBlueprint, contentGO.transform);
            plantBlueprintSlot.SetData(slot);
        }
    }
}
