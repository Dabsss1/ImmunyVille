using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "HungerThirst/Create new icon")]
public class HungerThirstItem : ScriptableObject
{
    [Header("Hunger")]
    public Sprite hungerFilledIcon;
    public Sprite hungerEmptyIcon;

    [Header("Thirst")]
    public Sprite thirstFilledIcon;
    public Sprite thirstEmptyIcon;
}
