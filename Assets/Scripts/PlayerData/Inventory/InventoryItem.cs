using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create new inventory item")]
[System.Serializable]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;

    public float itemPrice;

    public bool consumable;

    [Header("Stats")]
    public float health;
    public float body;
    public float confidence;
    public float strength;

    [Header("Hunger and Thirst")]
    public float hunger;
    public float thirst;

}
