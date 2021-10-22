using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BadgeBlueprint : MonoBehaviour
{
    public Image badgeIcon;
    public TextMeshProUGUI badgeName;

    // Update is called once per frame
    public void SetData(BadgeSlot badge)
    {
        Debug.Log(badge.badge.badgeName + " tier " + badge.tier);

        badgeName.text = badge.badge.badgeName;

        switch (badge.tier)
        {
            case 1:
                badgeIcon.sprite = badge.badge.bronzeIcon;
                break;
            case 2:
                badgeIcon.sprite = badge.badge.silverIcon;
                break;
            case 3:
                badgeIcon.sprite = badge.badge.goldIcon;
                break;
            default:
                break;
        }
    }
}
