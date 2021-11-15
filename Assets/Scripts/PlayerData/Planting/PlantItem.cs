using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = ("plant/Create new plant item"))]
[System.Serializable]
public class PlantItem : ScriptableObject
{
    public Sprite plant;
    public Sprite seed;
    public string plantName;
    public int daysToGrow;

    public float goldAmount;
    public float seedPrice;
}
