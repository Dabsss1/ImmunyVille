using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "badge/Create new badge item")]
public class BadgeItem : ScriptableObject
{
    public string badgeName;

    [Header("Bronze")]
    public Sprite bronzeIcon;
    public string bronzeDescription;

    [Header("Silver")]
    public Sprite silverIcon;
    public string silverDescription;

    [Header("Gold")]
    public Sprite goldIcon;
    public string goldDescription;
}
