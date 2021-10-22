using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeUI : MonoBehaviour
{
    public GameObject contentGO;

    public BadgeBlueprint badgeBlueprint;

    public static BadgeUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateBadgeUI();
    }

    void UpdateBadgeUI()
    {
        foreach (Transform child in contentGO.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (BadgeSlot slot in Badges.Instance.badgeSlots)
        {
            if (slot.tier == 0)
                continue;

            BadgeBlueprint badgeBlueprintSlot = Instantiate(badgeBlueprint, contentGO.transform);
            badgeBlueprintSlot.SetData(slot);
        }
    }
}
