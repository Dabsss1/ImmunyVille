using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badges : MonoBehaviour
{
    public List<BadgeSlot> badgeSlots;

    public static Badges Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ResetData()
    {
        foreach (BadgeSlot slot in badgeSlots)
        {
            slot.tier = 0;
        }
    }

    public void ObtainBadge(string badgeName, int tier)
    {
        foreach (BadgeSlot badgeSlot in badgeSlots)
        {
            if (badgeSlot.badge.badgeName == badgeName)
            {
                badgeSlot.tier = tier;
                return;
            }
        }
    }
}

[System.Serializable]
public class BadgeSlot
{
    public BadgeItem badge;
    public int tier;
}
